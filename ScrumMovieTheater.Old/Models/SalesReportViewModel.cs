using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScrumMovieTheater.Old.Models
{
    public class SalesReportViewModel
    {
        public string TheaterName { get; set; } = "";

        public int TicketsSold { get; set; }

        public decimal Revenue { get; set; }
    }
}