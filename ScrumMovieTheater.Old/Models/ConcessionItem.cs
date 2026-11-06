using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumMovieTheater.Old.Models
{
    [Table("concessionitems")]
    public class ConcessionItem
    {
        [Key]
        public int ConcessionItemId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? Category { get; set; }

        public bool Active { get; set; } = true;

        public int LowStockThreshold { get; set; } = 10;

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();

    }
}