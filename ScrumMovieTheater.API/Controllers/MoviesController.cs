using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.API.Models;
using ScrumMovieTheater.DTOs;
using ScrumMovieTheater.API.Providers;


namespace ScrumMovieTheater.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // we must create a variable that is capable of holding a ScrumMovieTheaterContext type with _
        // name things better at the start. 
        private readonly ScrumMovieTheaterContext _movieContext; 
        // in this constructor we need to provide the corresponding db context found in the providers folder.
        public MoviesController(ScrumMovieTheaterContext movieContext) {
            _movieContext = movieContext; 
        }

        
        [HttpGet]
        public List<Movie> Get()
        {
            return _movieContext.Movies.ToList(); 
        }

        // understand what you are transmitting and use vs codes split view to see what fields the transmitted DTO/model has. 
        [HttpPost]
        public async Task Post(Movie movie) {
            _movieContext.Movies.Add(movie);
            _movieContext.SaveChanges(); 
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] Movie updatedMovie) 
        {
            var movieToUpdate = _movieContext.Movies.FirstOrDefault(m => m.IdMovie.Equals(id));
            if (movieToUpdate == null)
                return;
            // do not tell asp.net to update id fields. let the database handle that. 
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.Description = updatedMovie.Description;
            movieToUpdate.RuntimeMinutes = updatedMovie.RuntimeMinutes;
            movieToUpdate.Genre = updatedMovie.Genre;
            movieToUpdate.Rating = updatedMovie.Rating; 

            _movieContext.Movies.Update(movieToUpdate);
            await _movieContext.SaveChangesAsync();
        }

        // DELETE api/<EventsController>/5
        // DATABASE NEEDS TO BE TURNED ON IN ORDER TO RUN ANY OF THESE METHODS. 
        [HttpDelete]
        public async Task Delete(int id)
        {
            var movieToDelete = _movieContext.Movies.FirstOrDefault(p => p.IdMovie.Equals(id));
            if (movieToDelete == null)
                return;
            _movieContext.Movies.Remove(movieToDelete);
            _movieContext.SaveChanges();
        }


    }
}
