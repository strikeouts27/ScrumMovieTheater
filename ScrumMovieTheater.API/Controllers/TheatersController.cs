using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.API.Models;
using ScrumMovieTheater.API.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        // we must create a variable that is capable of holding a ScrumMovieTheaterContext type with _
        // name things better at the start. 
        private readonly ScrumMovieTheaterContext _theaterContext;
        // in this constructor we need to provide the corresponding db context found in the providers folder.
        public TheatersController(ScrumMovieTheaterContext theaterContext)
        {
            _theaterContext = theaterContext;
        }

        // GET: api/<TheaterController>
        [HttpGet]
        public List<Theater> GetAll()
        {
            return _theaterContext.Theaters.ToList();
        }
        
    }
}
