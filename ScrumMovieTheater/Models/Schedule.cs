namespace ScrumMovieTheater.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string TheaterName { get; set; }
        public string MovieTitle { get; set; }
        public DateTime Showtime { get; set; }
    }
}