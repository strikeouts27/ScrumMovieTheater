namespace ScrumMovieTheater.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string CustomerName { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookedAt { get; set; }
    }
}