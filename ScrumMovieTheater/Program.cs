using ScrumMovieTheater.Data;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient("ScrumMovieTheaterAPI", client =>
{
    // The BaseAddress allows you to specify what the base address is for the entire project.
    // The base address is the staring point for the api. 
    client.BaseAddress = new Uri("http://localhost:5013/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// how to make Program.cs aware of the MovieDataService class. 
builder.Services.AddScoped<MovieDataService>();

// how to make Program.cs aware of the TheaterDataService class. 
builder.Services.AddScoped<TheaterDataService>();

// how to make Program.cs aware of the ShowroomDataService class. 
builder.Services.AddScoped<ShowroomDataService>(); 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Swagger UI (Development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
