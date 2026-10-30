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
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<ConcessionItem> ConcessionItems { get; set; }

        public DbSet<ConcessionInventory> ConcessionInventories { get; set; }

        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        // added by Eugene
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        // Override the OnModelCreating method to configure the database schema
        // This method is called when the model is being created/initialized
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Theater>().ToTable("theater");
            modelBuilder.Entity<Movie>().ToTable("movie");
            modelBuilder.Entity<Showtime>().ToTable("showtimes");
            // added by Eugene
            modelBuilder.Entity<Booking>().ToTable("bookings");
            modelBuilder.Entity<Auditorium>().ToTable("auditorium");
            modelBuilder.Entity<ConcessionItem>().ToTable("concessionitems");
            modelBuilder.Entity<ConcessionInventory>().ToTable("concessioninventory");
            modelBuilder.Entity<InventoryTransaction>().ToTable("inventorytransactions");

            // added by Eugene
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("orderitems");

            modelBuilder.Entity<Theater>()
                .HasKey(t => t.TheaterId);

            // DB Context -> Theater Configuration
            modelBuilder.Entity<Theater>()
                .HasData(
                    new Theater 
                    { 
                        TheaterId = 1, 
                        Name = "Richardson",
                        Address = "100 S Central Expy",
                        Description = "Our first theater",
                        Active = true
                    }                  
                );

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieId);

            modelBuilder.Entity<Auditorium>()
               .HasKey(a => a.AuditoriumId);
               // Added by Eugene
            modelBuilder.Entity<ConcessionItem>()
               .HasKey(c => c.ConcessionItemId);

            modelBuilder.Entity<Order>()
               .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
               .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
                

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Theater)              // FIX HERE
                .WithMany(t => t.Showtimes)         // (assuming Theater has Showtimes list)
                .HasForeignKey(s => s.TheaterId);   // correct FK

            modelBuilder.Entity<Showtime>()
               .HasOne(s => s.Auditorium)
               .WithMany()
               .HasForeignKey(s => s.AuditoriumId);

            modelBuilder.Entity<Auditorium>()
               .HasOne(a => a.Theater)
               .WithMany(t => t.Auditoriums)
               .HasForeignKey(a => a.TheaterId);

            modelBuilder.Entity<ConcessionItem>()
              .HasKey(c => c.ConcessionItemId);

            modelBuilder.Entity<ConcessionInventory>()
             .HasOne(i => i.ConcessionItem)
             .WithMany()
             .HasForeignKey(i => i.ConcessionItemId);

           modelBuilder.Entity<ConcessionInventory>()
                .HasOne(i => i.Theater)
                .WithMany()
                .HasForeignKey(i => i.TheaterId);

                
               // added by Eugene
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Booking)
                .WithMany()
                .HasForeignKey(o => o.BookingId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ConcessionItem)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.ConcessionItemId);   
        }
    }
}