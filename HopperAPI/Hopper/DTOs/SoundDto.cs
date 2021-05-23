
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hopper.Models;

namespace Hopper.DTOs
{
    public class SoundDto:SoundEditDto
    {
        public SoundDto(Sound sound,ApplicationUser user)  :base(sound)
        {
            if (sound == null)
                throw new ArgumentNullException(nameof(sound));
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Likes = sound.LikedUsers.Count;
            Liked = sound.LikedUsers.Any(l => l.Id == user.Id);
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