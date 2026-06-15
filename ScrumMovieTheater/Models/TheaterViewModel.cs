using ScrumMovieTheater.DTOs;
using System.Reflection; 

namespace ScrumMovieTheater.Models;

    // This MovieListViewModel will contain a list of Movies that are of the same type

    // as the class below MovieListItemViewModel.

    public class TheaterListViewModel
    {
        public List<TheaterViewModel> TheaterList;

        public TheaterListViewModel(List<TheaterDTO> theaterDTOs)
        {
            TheaterList = new List<TheaterViewModel>(); 

            foreach(var theater in theaterDTOs)
            {
                TheaterList.Add(
                    new TheaterViewModel
                    {
                        Id = theater.Id,
                        Name = theater.Name,
                        Address = theater.Address,
                    Description = theater.Description,
                    }
                    );
            }

        }
    }
    // These properties must match exactly what is in the database tables. 
    public class TheaterViewModel()
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
    }
