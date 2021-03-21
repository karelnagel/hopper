using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Hopper.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Hopper.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        [ProtectedPersonalData]
        public string LastName { get; set; }

        public UserLanguage Language { get; set; }

        [ForeignKey("ApplicationUserId")]
        public List<Sound> CreatedSounds { get; set; }

        public List<Favorite> Favorites { get; set; }

        public void Update(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            Language = userDto.Language;
        }
    }

    public enum UserLanguage
    {
        English,
        Eesti
    }
}