using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int RuntimeMinutes { get; set; }

    public string? Genre { get; set; }

    public string? Rating { get; set; }
}

