namespace ScrumMovieTheater.Models;

public class Booking
{
    public int Id { get; set; }

    public int ShowtimeId { get; set; }
    public Showtime Showtime { get; set; }

    public string CustomerName { get; set; }

    public int Adults { get; set; }
    public int Kids { get; set; }

    public int TotalPrice { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.Now;
}
