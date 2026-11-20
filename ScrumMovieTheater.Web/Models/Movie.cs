namespace ScrumMovieTheater.Web.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; } = "";
        public string Rating { get; set; } = "";
        public string Genre { get; set; } = "";
        public int RuntimeMinutes { get; set; }
    }
}