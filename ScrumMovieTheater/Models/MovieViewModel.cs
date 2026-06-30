using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 600)]
        [Display(Name = "Duration (minutes)")]
        public int RuntimeMinutes  { get; set; }

    }
}