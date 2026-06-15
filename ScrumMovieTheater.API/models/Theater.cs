using System;
using System.Collections.Generic;

namespace ScrumMovieTheater.API.Models;

public partial class Theater
{
    public int IdTheater { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Showroom> Showrooms { get; set; } = new List<Showroom>();
}
