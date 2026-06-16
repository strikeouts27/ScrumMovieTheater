namespace ScrumMovieTheater.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Rating { get; set; }
        public required string Genre { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required int RuntimeMinutes { get; set; }
    }
}
