using System.Collections.Generic;

namespace ScrumMovieTheater.Old.Models
{
    public class HomeViewModel
    {
        public List<Movie> NowShowing { get; set; } = new List<Movie>();
        public List<Movie> ComingSoon { get; set; } = new List<Movie>();
    }
}