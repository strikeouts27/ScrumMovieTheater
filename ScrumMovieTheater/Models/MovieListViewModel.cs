using ScrumMovieTheater.DTOs;
using System.Reflection;

namespace ScrumMovieTheater.Models
{
    // This MovieListViewModel will contain a list of Movies that are of the same type
    // as the class below MovieListItemViewModel.
    public class MovieListViewModel
    {
        // private usage would require _ and a lower case naming convention. 
        // public would use a capital letter and Pascal case without an _ used.
        public List<MovieListItemViewModel> MovieList;
    
        // this code converts the DTO's into a view model format that the views can use.
        public MovieListViewModel(List<MovieDTO> movieDTOs)
        {
            MovieList = new List<MovieListItemViewModel>();
             
            foreach(var movie in movieDTOs) {
                // what specifically is being converted and the how
                MovieList.Add(
                    new MovieListItemViewModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        ReleaseDate = movie.ReleaseDate,
                        // this specifies the url for using this image in the front end
                        // and how the data is in the database is how this code will interpret what to do. 
                        // for example JohnWick does not work because the database has it as John Wick
                        // John Wick.jpg is the correct answer.
                        ImageUrl = movie.Title + ".jpg",

                    }
                );

            }
        }

        

    }
    // might want to pull up the models of what you are making a view model for
    // so that you choose correct attributes. use vs code split view. 

    // TO DO SPEAK WITH THE FRONT END ABOUT WHAT WE ARE USING AND WHAT WE ARE NOT USING. 
    public class MovieListItemViewModel()
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        // probably can use this url with an image we include as an asset for the project. 
        public string ImageUrl { get; set; }

    }
}
