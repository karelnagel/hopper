using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Hopper.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        [ProtectedPersonalData]
        public string LastName { get; set; }

        public string Language { get; set; }

        [ForeignKey("ApplicationUserId")]
        public List<Sound> CreatedSounds { get; set; }

        public List<Favorite> Favorites { get; set; }
    }
}