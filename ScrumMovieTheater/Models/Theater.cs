using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ScrumMovieTheater.Models
{
    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<Showtime> Showtimes { get; set; }
    }
} 

