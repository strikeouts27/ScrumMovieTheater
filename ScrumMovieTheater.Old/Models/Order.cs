namespace ScrumMovieTheater.Old.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int BookingId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public Booking? Booking { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}