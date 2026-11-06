using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ScrumMovieTheater.Old.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int TheaterId { get; set; }
        public Theater? Theater { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium? Auditorium { get; set; }
        public DateTime ShowDate { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public decimal Price { get; set; }
    }
}