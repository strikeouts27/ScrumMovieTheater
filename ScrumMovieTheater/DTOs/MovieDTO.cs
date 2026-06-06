namespace ScrumMovieTheater.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }

    }
}
