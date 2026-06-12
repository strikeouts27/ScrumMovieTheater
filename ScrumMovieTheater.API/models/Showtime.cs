using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.models;

public partial class Showtime
{
    public int idShowtime { get; set; }

    public int MovieId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal TicketPrice { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
