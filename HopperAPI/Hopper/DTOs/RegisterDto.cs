using System.ComponentModel.DataAnnotations;

namespace Hopper.DTOs
{
    public class RegisterDto : LoginDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Language { get; set; }
    }
}