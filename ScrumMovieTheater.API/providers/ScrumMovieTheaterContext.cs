using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.API.models;

namespace ScrumMovieTheater.API.providers;

public partial class ScrumMovieTheaterContext : DbContext
{
    // IConfiguration is  a part of the asp.net domain. it is a service that provided by dotnet. 
    // IConfiguration grabs information from appsettings.json
    // appsettings.json is a file that you can input any settings that you need. 
    private readonly IConfiguration _config; 
    public ScrumMovieTheaterContext()
    {
    }

    public ScrumMovieTheaterContext(DbContextOptions<ScrumMovieTheaterContext> options, IConfiguration config)
        : base(options)
    {
        _config = config; 
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            
            optionsBuilder.UseMySQL(_config.GetConnectionString("DefaultConnection")); 
        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Rating).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.ShowtimeId).HasName("PRIMARY");

            entity.ToTable("showtimes");

            entity.HasIndex(e => e.MovieId, "MovieId");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.TicketPrice).HasPrecision(5);

            //entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
            //    .HasForeignKey(d => d.MovieId)
                //.HasConstraintName("showtimes_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
