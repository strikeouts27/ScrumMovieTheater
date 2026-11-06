namespace ScrumMovieTheater.Old.Models
{
    public class ConcessionCartItem
    {
        public int ConcessionItemId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Subtotal => Price * Quantity;
    }
}