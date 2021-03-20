using System;
using System.ComponentModel.DataAnnotations;
using Hopper.Models;

namespace Hopper.DTOs
{
    public class SoundEditDto
    {
        public SoundEditDto(Sound sound)
        {
            if (sound == null)
                throw new ArgumentNullException(nameof(sound));

            Id = sound.Id;
            Title = sound.Title;
            Author = sound.Author;
            Video = sound.Video;
            Language = sound.Language;
        }

        public SoundEditDto()
        {
        }

        public Guid? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Video { get; set; }

        [Required]
        public string Language { get; set; }

    }
}