using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Services;

public interface IMovieCatalog
{
    IReadOnlyList<MovieViewModel> GetAll();
    void Add(MovieViewModel movie);
}
