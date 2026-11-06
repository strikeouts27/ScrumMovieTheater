using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumMovieTheater.Old.Models
{
    [Table("concessioninventory")]
    public class ConcessionInventory
    {
        [Key]
        public int InventoryId { get; set; }

        public int ConcessionItemId { get; set; }

        public ConcessionItem? ConcessionItem { get; set; }


        public int TheaterId { get; set; }

        public Theater? Theater { get; set; }


        public int QuantityOnHand { get; set; }
    }
}