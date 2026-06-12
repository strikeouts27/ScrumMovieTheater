namespace ScrumMovieTheater.API.models
{
    public class Showroom
    {
        public int idShowroom { get; set; }
        public int Capacity { get; set; }
        public Movie Movie { get; set; }
        public string ShowroomNumber { get; set; }
        public Theater Theater { get; set; }
    }
}
