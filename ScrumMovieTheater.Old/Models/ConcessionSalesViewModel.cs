namespace ScrumMovieTheater.Old.Models
{
    public class ConcessionSalesViewModel
    {
        public int OrderId { get; set; }

        public int BookingId { get; set; }

        public string ItemName { get; set; } = "";

        public int QuantitySold { get; set; }

        public decimal Price { get; set; }

        public decimal Revenue { get; set; }
    }
}