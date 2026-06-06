using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.API.models;
using ScrumMovieTheater.API.providers;

namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // we must create a variable that is capable of holding a ScrumMovieTheaterContext type with _
        private readonly ScrumMovieTheaterContext _moviecontext; 
        // in this constructor we need to provide the corresponding db context found in the providers folder.
        public MoviesController(ScrumMovieTheaterContext moviecontext) {
            _moviecontext = moviecontext; 
        }

        
        [HttpGet]
        public List<Movie> Get()
        {
            return _moviecontext.Movies.ToList(); 
        }

        [HttpPost]
        public async Task Post(Movie movie) {
            _moviecontext.Movies.Add(movie);
            _moviecontext.SaveChanges(); 
        }



        
    }

   
    
}
