using ScrumMovieTheater.API.providers;
using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.API.models;
[Route("api/showtimes")]
[ApiController]
public class ShowtimesApiController : ControllerBase
{
    private readonly ScrumMovieTheaterContext _context;

    public ShowtimesApiController(ScrumMovieTheaterContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Showtime> Get()
    {
        return _context.Showtimes.ToList();
    }
}