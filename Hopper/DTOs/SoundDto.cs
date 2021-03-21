using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hopper.Models;

namespace Hopper.DTOs
{
    public class SoundDto : SoundEditDto
    {
        public SoundDto(Sound sound, ApplicationUser user) : base(sound)
        {
            if (sound == null)
                throw new ArgumentNullException(nameof(sound));
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Likes = sound.Favorites.Count;
            Liked = sound.Favorites.Any(l => l.ApplicationUserId == user.Id);
            Address = sound.Address;
            Creator = sound.ApplicationUserId == user.Id;
        }

        public SoundDto()
        {
        }


        [Required]
        public int Likes { get; set; }

        [Required]
        public bool Liked { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool Creator { get; set; }
    }
}