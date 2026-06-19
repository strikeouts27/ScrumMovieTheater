using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.API.models;
using ScrumMovieTheater.API.providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterApiController : ControllerBase
    {
        // we must create a variable that is capable of holding a ScrumMovieTheaterContext type with _
        // name things better at the start. 
        private readonly ScrumMovieTheaterContext _theaterContext;
        // in this constructor we need to provide the corresponding db context found in the providers folder.
        public TheaterApiController(ScrumMovieTheaterContext theaterContext)
        {
            _theaterContext = theaterContext;
        }

        // GET: api/<TheaterController>
        [HttpGet]
        public List<Theater> Get()
        {
            return _theaterContext.Theaters.ToList();
        }
    }
}
