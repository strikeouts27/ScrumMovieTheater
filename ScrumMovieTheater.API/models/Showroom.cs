using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.Models;

public partial class Showroom
{
    public int IdShowroom { get; set; }

    public int Capacity { get; set; }

    public int Movie { get; set; }

    public string Name { get; set; } = null!;

    public int Theater { get; set; }

    public virtual Movie MovieNavigation { get; set; } = null!;

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual Theater TheaterNavigation { get; set; } = null!;
}
