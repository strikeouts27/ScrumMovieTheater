
using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Movie Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Display(Name = "Duration")]
        public TimeSpan Duration { get; set; }

        [Range(0, 20)]
        [Display(Name = "Hours")]
        public int DurationHours { get; set; }

        [Range(0, 59)]
        [Display(Name = "Minutes")]
        public int DurationMinutes { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;
    }
}