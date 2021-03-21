using System;
using System.Collections.Generic;
using Hopper.DTOs;

namespace Hopper.Models
{
    public class Sound
    {
        public Sound(SoundEditDto soundEditDto)
        {
            if (soundEditDto == null)
                throw new ArgumentNullException(nameof(soundEditDto));

            Title = soundEditDto.Title;
            Author = soundEditDto.Author;
            Video = soundEditDto.Video;
            Language = soundEditDto.Language;
        }

        public Sound()
        {
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Video { get; set; }

        public UserLanguage Language { get; set; }

        public Guid ApplicationUserId { get; set; }

        public List<Favorite> Favorites { get; set; }

        public string Address { get; set; }

        public void Update(SoundEditDto soundEditDto)
        {
            if (soundEditDto == null)
                throw new ArgumentNullException(nameof(soundEditDto));

            Title = soundEditDto.Title;
            Author = soundEditDto.Author;
            Video = soundEditDto.Video;
            Language = soundEditDto.Language;
        }
    }
}