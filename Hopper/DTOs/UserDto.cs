using System;
using System.ComponentModel.DataAnnotations;
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
        public UserLanguage Language { get; set; }
    }
}