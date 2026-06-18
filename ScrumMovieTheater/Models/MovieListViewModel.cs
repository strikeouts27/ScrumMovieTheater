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
        public List<MovieViewModel> MovieList;
    
        // this code converts the DTO's into a view model format that the views can use.
        public MovieListViewModel(List<MovieDTO> movieDTOs)
        {
            // we place the newly created object here. 
            MovieList = new List<MovieViewModel>();
             
            foreach(var movie in movieDTOs) {
                // what specifically is being converted and the how
                MovieList.Add(
                    new MovieViewModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        Rating = movie.Rating,
                        Genre = movie.Genre,
                        ReleaseDate = movie.ReleaseDate,
                        RuntimeMinutes = movie.RuntimeMinutes,


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

}
