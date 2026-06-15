using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.API.Models;
using ScrumMovieTheater.API.Providers; 

namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowroomController : ControllerBase 
    {
        // we must create a variable that is capable of holding a ScrumMovieTheaterContext type with _
        // name things better at the start. 
        private readonly ScrumMovieTheaterContext _showroomContext;
        
        public ShowroomController (ScrumMovieTheaterContext showContext)
        {
            _showroomContext = showContext; 
        }

        // FRONT TO BACK AND BACK TO FRONT METHODOLOGY 

        // APICONTROLLER -> MainProjectServices->Controller->ViewModel->View
        // GET: api/<ShowtimeController 
        [HttpGet]
        public List<Showroom> GetAll()
        {
            return _showroomContext.Showrooms.ToList(); 
        }
    }
}
