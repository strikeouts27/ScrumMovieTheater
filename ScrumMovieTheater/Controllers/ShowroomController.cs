using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.DTOs;
using ScrumMovieTheater.Models;
using ScrumMovieTheater.Services;
using System.Security.Cryptography.X509Certificates;


namespace ScrumMovieTheater.Controllers
{
    public class ShowroomController : Controller
    {
        private readonly ShowroomDataService _showroomDataService;
        // once you create this service you must tell Program.cs that this service exists. 
        public ShowroomController(ShowroomDataService showroomData)
        {
            _showroomDataService = showroomData;
        }


        [HttpGet]
        // This is a get method 

        public async Task<IActionResult> Index()
        {
            List<ShowroomDTO> showroom = await _showroomDataService.GetShowroomsAsync();

            // we use <> for templates. 
            // new movie list view model 1. use a viewmodel type 2. name the variable. 3 use the new keyword 4. call the list of movies, by calling the constructor you create an object with the infomration you need.

            ShowroomListViewModel showroomTemplate = new ShowroomListViewModel(showroom);
            return View(showroomTemplate); 
        }
    }
}