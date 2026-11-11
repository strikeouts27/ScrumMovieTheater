using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace ScrumMovieTheater.Old.Models
{
    public class Auditorium
    {
        public int AuditoriumId { get; set; }

        public int TheaterId { get; set; }

        public string Name { get; set; } = "";

        public int Capacity { get; set; }

        public bool Active { get; set; }

        public Theater? Theater { get; set; }
    }
}