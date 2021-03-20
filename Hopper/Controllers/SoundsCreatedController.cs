using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hopper.Data;
using Hopper.DTOs;
using Hopper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Hopper.Controllers
{
    [OpenApiTag("Sounds Created")]
    [Authorize]
    [Route("api/sounds/created")]
    [ApiController]
    public class SoundsCreatedController : ApiBaseController
    {
        public SoundsCreatedController(ApplicationDbContext context, IAuthorizationService authorizationService) : base(
            context, authorizationService)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoundDto>>> GetSounds()
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();

            return user.CreatedSounds.Select(s => new SoundDto(s,user)).ToList();
        }

        [HttpGet("{soundId}")]
        public async Task<ActionResult<SoundDto>> GetSound(Guid soundId)
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = GetCreatedSound(soundId, user);
            if (sound == null)
                return NotFound();

            return new SoundDto(sound,user);
        }


        [HttpPut("{soundId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutSound(Guid soundId, SoundEditDto soundEditDto)
        {
            if (soundEditDto == null)
                throw new ArgumentNullException(nameof(soundEditDto));

            if (soundId != soundEditDto.Id)
                return BadRequest();

            var user = await GetUser();
            var sound = GetCreatedSound(soundId, user);

            if (sound == null)
                return NotFound();

            sound.Update(soundEditDto);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SoundEditDto>> PostSound(SoundEditDto soundEditDto)
        {
            if (soundEditDto == null)
                throw new ArgumentNullException(nameof(soundEditDto));

            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = new Sound(soundEditDto);

            user.CreatedSounds.Add(sound);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetSound", new {id = sound.Id}, new SoundEditDto(sound));
        }
        
        [HttpPost("{soundId}/upload")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<string>> PostFile(Guid soundId, IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            
            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = GetCreatedSound(soundId, user);
            

            //Uploading file 
            if (file.Length <= 0) return BadRequest();
            var filePath = Path.Combine(Path.GetFullPath("/home/karel/Desktop"),soundId.ToString());
            System.Console.WriteLine(filePath);
            await using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
            
            sound.Address = filePath;
            await Context.SaveChangesAsync();
            
            return filePath;
        }

        [Authorize("Admin")]
        [HttpDelete("{soundId}")]
        public async Task<ActionResult<SoundEditDto>> DeleteSound(Guid soundId)
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = GetCreatedSound(soundId, user);
            if (sound == null)
                return NotFound();

            user.CreatedSounds.Remove(sound);
            await Context.SaveChangesAsync();

            return new SoundEditDto(sound);
        }

        private static Sound GetCreatedSound(Guid soundId, ApplicationUser user)
        {
            return user?.CreatedSounds.SingleOrDefault(s => s.Id == soundId);
        }
    }
}