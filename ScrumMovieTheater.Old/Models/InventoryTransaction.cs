using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumMovieTheater.Old.Models
{
    [Table("inventorytransactions")]
    public class InventoryTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int InventoryId { get; set; }

        public int QuantityChange { get; set; }

        public string? Reason { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int? EmployeeId { get; set; }


        public ConcessionInventory? Inventory { get; set; }
    }
}