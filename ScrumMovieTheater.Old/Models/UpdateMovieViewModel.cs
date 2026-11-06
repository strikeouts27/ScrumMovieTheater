using System;
using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Old.Models
{
    public class UpdateMovieViewModel
    {
        // Movie
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string Genre { get; set; } = "";

        public int RuntimeMinutes { get; set; }

        public string Rating { get; set; } = "";

        public DateTime ReleaseDate { get; set; }

        public string ImageUrl { get; set; } = "";

    }
}