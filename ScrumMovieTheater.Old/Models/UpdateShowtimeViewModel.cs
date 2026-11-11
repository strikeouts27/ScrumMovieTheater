using System;
using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Old.Models
{
    public class UpdateShowtimeViewModel
    {
        public int ShowtimeId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int TheaterId { get; set; }

        [Required]
        public int AuditoriumId { get; set; }

        [Required]
        public DateTime ShowDate { get; set; }

        [Required]
        public TimeSpan TimeSlot { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}