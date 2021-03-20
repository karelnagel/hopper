using System;

namespace Hopper.Models
{
    public class Favorite
    {
        
        public Guid Id { get; set; }

        public Sound Sound { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
        
        public Guid ApplicationUserId { get; set; }

        public Guid SoundId { get; set; }
    }
}