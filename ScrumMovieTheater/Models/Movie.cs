using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScrumMovieTheater.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; } = "";
        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

        public String Rating { get; set; } = "";
        public string Genre { get; set; } = "";
        public int RuntimeMinutes { get; set; }
    }
}