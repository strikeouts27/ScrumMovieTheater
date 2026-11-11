namespace ScrumMovieTheater.Old.Models
{
    public class KioskMenuViewModel
    {
        public List<ConcessionItem> Concessions { get; set; }
            = new List<ConcessionItem>();

        public List<ConcessionCartItem> CartItems { get; set; }
            = new List<ConcessionCartItem>();

        public int CartCount =>
            CartItems.Sum(item => item.Quantity);

        public decimal Total =>
            CartItems.Sum(item => item.Subtotal);
    }
}