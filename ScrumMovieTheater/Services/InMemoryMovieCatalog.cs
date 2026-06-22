using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Services;

public class InMemoryMovieCatalog : IMovieCatalog
{
    private readonly List<MovieViewModel> movies = new();

    public IReadOnlyList<MovieViewModel> GetAll()
    {
        return movies
            .OrderBy(movie => movie.Title)
            .ToList();
    }

    public void Add(MovieViewModel movie)
    {
        var newMovie = new MovieViewModel
        {
            Id = movie.Id == 0 ? movies.Count + 1 : movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            //Duration = movie.Duration,
            Description = movie.Description
        };

        movies.Add(newMovie);
    }
}
