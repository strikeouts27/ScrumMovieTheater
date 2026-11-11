namespace ScrumMovieTheater.Old.Models
{
    public class TheaterViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
    }
}