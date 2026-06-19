using System.ComponentModel.DataAnnotations.Schema;
using ScrumMovieTheater.Models;
public class Showtime
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int TheaterId { get; set; }
    public Theater Theater { get; set; } 
    public TimeSpan TimeSlot { get; set; }
    public int Price { get; set; }
}