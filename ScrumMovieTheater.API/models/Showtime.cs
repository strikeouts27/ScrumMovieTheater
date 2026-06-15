using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.models;

public partial class Showtime
{
    public int IdShowtimes { get; set; }

    public TimeSpan TimeSlot { get; set; }

    public int Showroom { get; set; }

    public int Movie { get; set; }

    public virtual Movie MovieNavigation { get; set; } = null!;

    public virtual Showroom ShowroomNavigation { get; set; } = null!;
}
