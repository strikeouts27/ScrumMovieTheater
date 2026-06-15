using Microsoft.AspNetCore.Mvc;
using ScrumMovieTheater.DTOs;
using System.Reflection;


namespace ScrumMovieTheater.Models; 

    public class ShowroomListViewModel
    {
    public List<ShowroomViewModel> ShowroomList;

    public ShowroomListViewModel(List<ShowroomDTO> showroomDTOs)
    {
        ShowroomList = new List<ShowroomViewModel>(); 

        foreach (var showroom in showroomDTOs)
        {
            ShowroomList.Add(
                new ShowroomViewModel
                {
                    idShowroom = showroom.Id,
                    Capacity = showroom.Capacity,
                    Movie = showroom.Movie,
                    Theater = showroom.Theater,

                }
                ); 
        }
    } 
    }
// this must match the actual database i suggest having mysql up to view the tables. 
    public class ShowroomViewModel()
    {
        public required int idShowroom { get; set; }
        public required int Capacity { get; set; }
        public required int Movie { get; set; }
        public required int Theater { get; set; }
    }
