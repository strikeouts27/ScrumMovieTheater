using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using ScrumMovieTheater.API.Models;

public class ShowtimeDTO
{
    public int Id { get; set; }
    public required Time Title { get; set; }
    public required int Showroom { get; set; }
    public required int Movie { get; set; }
    
}
