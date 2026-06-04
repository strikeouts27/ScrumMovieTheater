using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Models;

namespace ScrumMovieTheater.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Movie> Movies { get; set; }

    }
}