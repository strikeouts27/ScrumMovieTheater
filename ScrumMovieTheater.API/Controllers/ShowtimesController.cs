using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.API.Models;
using ScrumMovieTheater.API.Providers;
using System.Security.Cryptography.X509Certificates;


namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimesController : ControllerBase
    {
        // create a variable capable of holding a db Context object. 
        private readonly ScrumMovieTheaterContext _showtimeContext;

        // in this constructor we need to provide the corresponding db context found in the providers folder.
        public ShowtimesController(ScrumMovieTheaterContext showtimeContext)
        {
            _showtimeContext = showtimeContext; 
        }

        [HttpGet]
        public List<Showtime> Get()
        {
            return _showtimeContext.Showtimes.ToList();
        }

        // do not tell asp.net to update id fields. let the database handle that. 

        [HttpPost]
        public async Task Post(Showtime showtime)
        {
            _showtimeContext.Showtimes.Add(showtime);
            _showtimeContext.SaveChanges();
        }

        //[HttpPut("{id}")]
        //public async Task Update(int id, [FromBody] Showtime updatedShowtime)
        //{
        //    var showtimeToUpdate = _showtimeContext.Showtimes.FirstOrDefault(s => s.IdShowtimes.Equals(id));
        //    if (showtimeToUpdate == null)
        //        return;

        //    // let the database handle id's 
        //    showtimeToUpdate.StartTime = updatedShowtime.StartTime;
        //    showtimeToUpdate.EndTime = updatedShowtime.EndTime;
        //    showtimeToUpdate.TicketPrice = updatedShowtime.TicketPrice;
        //    showtimeToUpdate.Movie = updatedShowtime.Movie;

        //    _showtimeContext.Showtimes.Update(showtimeToUpdate);
        //    await _showtimeContext.SaveChangesAsync(); 
        //}

        //[HttpDelete]
        //public async Task Delete(int id)
        //{
        //    var showtimeToDelete = _showtimeContext.Showtimes.FirstOrDefault(s => s.idShowtime.Equals(id));
        //    if (showtimeToDelete == null)
        //        return;
        //    _showtimeContext.Showtimes.Remove(showtimeToDelete);
        //    _showtimeContext.SaveChanges(); 

        //}
        

    }
}
