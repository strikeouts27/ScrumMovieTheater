using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.Models;

public partial class Movie
{
    public int IdMovie { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Rating { get; set; } = null!;

    public string? Genre { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? RuntimeMinutes { get; set; }

    public virtual ICollection<Showroom> Showrooms { get; set; } = new List<Showroom>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
