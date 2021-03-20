using System;
using System.Collections.Generic;
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
                .ThenInclude(s => s.Favorites)
                .Include(u => u.Favorites)
                .SingleOrDefaultAsync(u => u.Id == GetUserId());
            if (user == null)
                return NotFound();

            //Filter
            var sounds = filter switch
            {
                SoundFilter.Created => user.CreatedSounds,
                SoundFilter.Liked => user.Favorites.Select(f => f.Sound),
                _ => await Context.Sounds.Include(s => s.Favorites).Where(s => s.Address != null).ToListAsync(),
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
                SoundSortBy.Likes => sounds.OrderBy(s => s.Favorites.Count),
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

            var like = user.Favorites.SingleOrDefault(f => f.SoundId == soundId);
            if (like == null)
            {
                like = new Favorite()
                {
                    ApplicationUser = user,
                    Sound = sound,
                };
                user.Favorites.Add(like);
                sound.Favorites.Add(like);
            }
            else
            {
                user.Favorites.Remove(like);
                sound.Favorites.Remove(like);
            }

            await Context.SaveChangesAsync();
            return new SoundDto(sound, user);
        }


        private async Task<Sound> GetSound(Guid soundId)
        {
            return await Context.Sounds.Include(s => s.Favorites).SingleOrDefaultAsync(s => s.Id == soundId);
        }
    }
}