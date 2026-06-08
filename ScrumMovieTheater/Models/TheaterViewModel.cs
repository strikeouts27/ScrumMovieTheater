namespace Theater {
    public class TheaterViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public int Capacity { get; set; }
    }
}