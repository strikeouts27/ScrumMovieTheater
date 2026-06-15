using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.API.Models;

namespace ScrumMovieTheater.API.Providers;

public partial class ScrumMovieTheaterContext : DbContext
{
    public ScrumMovieTheaterContext()
    {
    }

    public ScrumMovieTheaterContext(DbContextOptions<ScrumMovieTheaterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Showroom> Showrooms { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;database=ScrumMovieTheater;uid=strikeouts27;password=Baseball100!!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.Property(e => e.IdMovie).HasColumnName("idMovie");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Genre).HasMaxLength(45);
            entity.Property(e => e.Rating).HasMaxLength(45);
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(45);
        });

        modelBuilder.Entity<Showroom>(entity =>
        {
            entity.HasKey(e => e.IdShowroom).HasName("PRIMARY");

            entity.ToTable("showrooms");

            entity.HasIndex(e => e.Movie, "FK_Movie_Showrooms_idx");

            entity.HasIndex(e => e.Theater, "FK_Theater_Showrooms_idx");

            entity.Property(e => e.IdShowroom).HasColumnName("idShowroom");
            entity.Property(e => e.Name).HasMaxLength(45);

            entity.HasOne(d => d.MovieNavigation).WithMany(p => p.Showrooms)
                .HasForeignKey(d => d.Movie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_Showrooms");

            entity.HasOne(d => d.TheaterNavigation).WithMany(p => p.Showrooms)
                .HasForeignKey(d => d.Theater)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Theater_Showrooms");
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.IdShowtimes).HasName("PRIMARY");

            entity.ToTable("showtimes");

            entity.HasIndex(e => e.Movie, "FK_Movie_idMovie_idx");

            entity.HasIndex(e => e.Showroom, "FK_Showroom_idShowroom_idx");

            entity.Property(e => e.IdShowtimes).HasColumnName("idShowtimes");
            entity.Property(e => e.TimeSlot).HasColumnType("time");

            entity.HasOne(d => d.MovieNavigation).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.Movie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_idMovie");

            entity.HasOne(d => d.ShowroomNavigation).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.Showroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Showroom_idShowroom");
        });

        modelBuilder.Entity<Theater>(entity =>
        {
            entity.HasKey(e => e.IdTheater).HasName("PRIMARY");

            entity.ToTable("theaters");

            entity.Property(e => e.IdTheater).HasColumnName("idTheater");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
