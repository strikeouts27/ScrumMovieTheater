using Microsoft.EntityFrameworkCore;

namespace ScrumMovieTheater.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            // Pass the options to the base DbContext class to configure the database connection
            : base(options)
    {
    }
}
