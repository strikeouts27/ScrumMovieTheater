namespace ScrumMovieTheater.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int RuntimeMinutes { get; set; }

    }
    // understand the product owners requirements and than hopefully your database or your code first code is correct. 
    // if code first than update the models if there are problems
    // if database first look at your models and your database for corrections. 

    public class CreateMovieDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

        public required int RuntimeMinutes { get; set; }
        public string? Genre { get; set; }
        public string? Rating { get; set; }
    }
}
