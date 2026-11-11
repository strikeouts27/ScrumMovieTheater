namespace ScrumMovieTheater.Old.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ConcessionItemId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Price { get; set; }

        public Order? Order { get; set; }

        public ConcessionItem? ConcessionItem { get; set; }
    }
}