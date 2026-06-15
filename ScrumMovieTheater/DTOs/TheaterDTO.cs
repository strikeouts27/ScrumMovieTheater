namespace ScrumMovieTheater.DTOs;

public class TheaterDTO
{
    // properties work better with json serializers. 
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Description { get; set; }
}