using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Old.Models
{
    public class ConcessionCheckoutViewModel
    {
        [Required]
        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }

        public List<ConcessionCartItem> CartItems { get; set; }
            = new List<ConcessionCartItem>();

        public decimal Total =>
            CartItems.Sum(item => item.Subtotal);
    }
}