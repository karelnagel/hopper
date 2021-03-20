using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hopper.Models;

namespace Hopper.DTOs
{
    public class UserDto
    {
        public UserDto(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            Id = applicationUser.Id;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            Language = applicationUser.Language;
            CreatedSounds = applicationUser.CreatedSounds?.Select(s => new SoundEditDto(s)).ToList();
            LikedSounds = applicationUser.Favorites?.Select(s => new SoundEditDto(s.Sound)).ToList();
        }

        public UserDto()
        {
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public List<SoundEditDto> CreatedSounds { get; set; }

        [Required]
        public List<SoundEditDto> LikedSounds { get; set; }
    }
}