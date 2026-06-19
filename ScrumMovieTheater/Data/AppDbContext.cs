// Import Entity Framework Core namespace
using Microsoft.EntityFrameworkCore;
// Import the Models namespace to access Movie, Booking, User classes
using ScrumMovieTheater.Models;

// Define the namespace for this Data context class
namespace ScrumMovieTheater.Data
{
    // Define the AppDbContext class that inherits from DbContext
    // DbContext is the core class that manages database operations
    public class AppDbContext : DbContext
    {
        // Constructor that accepts database configuration options
        // This is called when the context is instantiated via dependency injection
        public AppDbContext(DbContextOptions<AppDbContext> options)
            // Pass the options to the base DbContext class to configure the database connection
            : base(options)
        {
        }

        // DbSet represents a table in the database
        // This DbSet<Movie> allows you to query and save Movie records
        // You access it with _context.Movies
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; } // ADD THIS
        public DbSet<Showtime> Showtimes { get; set; }
        // Override the OnModelCreating method to configure the database schema
        // This method is called when the model is being created/initialized
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Theater>().ToTable("theater");
            modelBuilder.Entity<Movie>().ToTable("movie");
            modelBuilder.Entity<Showtime>().ToTable("showtimes");

            modelBuilder.Entity<Theater>()
                .HasKey(t => t.TheaterId);

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieId);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Theater)              // FIX HERE
                .WithMany(t => t.Showtimes)         // (assuming Theater has Showtimes list)
                .HasForeignKey(s => s.TheaterId);   // correct FK
        }
    }
}