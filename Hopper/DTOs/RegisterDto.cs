using System.ComponentModel.DataAnnotations;
using Hopper.Models;

namespace Hopper.DTOs
{
    public class RegisterDto : LoginDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public UserLanguage Language { get; set; }
    }
}