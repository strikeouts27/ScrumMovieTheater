namespace ScrumMovieTheater.Models
{
    // might want to pull up the models of what you are making a view model for
    // so that you choose correct attributes. use vs code split view. 

    // TO DO SPEAK WITH THE FRONT END ABOUT WHAT WE ARE USING AND WHAT WE ARE NOT USING. 
    public class MovieViewModel()
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RuntimeMinutes { get; set; }

        // probably can use this url with an image we include as an asset for the project. 
        public string ImageUrl { get; set; }

    }
}
