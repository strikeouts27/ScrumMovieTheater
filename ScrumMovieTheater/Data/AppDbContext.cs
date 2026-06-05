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

        // Add this parameterless constructor for migrations
        public AppDbContext()
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}