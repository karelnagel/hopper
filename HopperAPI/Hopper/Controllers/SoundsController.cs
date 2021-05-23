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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;

namespace Hopper.Controllers
{
    [OpenApiTag("Sounds")]
    [Authorize]
    [Route("api/sounds")]
    [ApiController]
    public class SoundsController : ApiBaseController
    {
        public SoundsController(ApplicationDbContext context, IAuthorizationService authorizationService) : base(
            context, authorizationService)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoundDto>>> GetSounds(
            [FromQuery] string searchBy,
            [FromQuery] string language,
            [FromQuery] SoundFilter? filter,
            [FromQuery] SoundSortBy? sortBy)
        {
            var user = await Context.Users.Include(u => u.CreatedSounds)
                .ThenInclude(s => s.LikedUsers)
                .Include(u => u.LikedSounds)
                .SingleOrDefaultAsync(u => u.Id == GetUserId());
            if (user == null)
                return NotFound();

            //Filter
            var sounds = filter switch
            {
                SoundFilter.Created => user.CreatedSounds,
                SoundFilter.Liked => user.LikedSounds.Select(f => f),
                _ => await Context.Sounds.Include(s => s.LikedUsers).Where(s => s.Address != null).ToListAsync(),
            };

            //Language
            if (!string.IsNullOrEmpty(language))
                sounds = sounds.Where(s => s.Language == language);

            //Search
            if (!string.IsNullOrEmpty(searchBy))
                sounds = sounds.Where(s =>
                    s.Title.Contains(searchBy, StringComparison.Ordinal) ||
                    s.Author.Contains(searchBy, StringComparison.Ordinal) ||
                    s.Video.Contains(searchBy, StringComparison.Ordinal));

            //Sort
            sounds = sortBy switch
            {
                SoundSortBy.Title => sounds.OrderBy(s => s.Title),
                SoundSortBy.Author => sounds.OrderBy(s => s.Author),
                SoundSortBy.Likes => sounds.OrderBy(s => s.LikedUsers.Count),
                _ => sounds
            };

            return sounds.Select(s => new SoundDto(s, user)).ToList();
        }

        public enum SoundFilter
        {
            Liked,
            Created,
        }

        public enum SoundSortBy
        {
            Title,
            Author,
            Likes,
        }

        [HttpGet("{soundId}")]
        public async Task<ActionResult<SoundDto>> GetSoundInfo(Guid soundId)
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = await GetSound(soundId);
            if (sound == null)
                return NotFound();

            return new SoundDto(sound, user);
        }

        [HttpGet("{soundId}/like")]
        public async Task<ActionResult<SoundDto>> LikeSound(Guid soundId)
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();

            var sound = await GetSound(soundId);
            if (sound == null)
                return NotFound();

            var like = user.LikedSounds.SingleOrDefault(f => f.Id == soundId);
            if (like == null)
            {
                user.LikedSounds.Add(sound);
                sound.LikedUsers.Add(user);
            }
            else
            {
                user.LikedSounds.Remove(sound);
                sound.LikedUsers.Remove(user);
            }

            await Context.SaveChangesAsync();
            return new SoundDto(sound, user);
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
            //Todo return saved sound
            //Todo enums as strings in controller
            return Ok();
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

        private async Task<Sound> GetSound(Guid soundId)
        {
            return await Context.Sounds.Include(s => s.LikedUsers).SingleOrDefaultAsync(s => s.Id == soundId);
        }
    }
}