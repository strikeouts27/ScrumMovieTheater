namespace ScrumMovieTheater.DTOs
    // DTO's should have copies in the main project and the api. 
{
    public class ShowroomDTO
    {
        public int Id { get; set; }
        public required int Capacity { get; set; }
        public required int Movie { get; set; }
        public required string Name { get; set; }
        public required int Theater { get; set; }
    }
}