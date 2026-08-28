using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ScrumMovieTheater.Data;
using ScrumMovieTheater.Helpers;
using ScrumMovieTheater.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services FIRST
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PLEASE NOTE THAT THIS DEVELOPER MODE WILL RUN ON BREAKPOINTS AND THAT YOU NEED TO TURN THE DEVELOPER MODE ON AND OFF WITH TRUE FALSE VALUES IN APP SETTINGS JSON. 

// the development auth handler is this class that we wrote in the Helpers folder under the file DevelopmentAuthHandler

if (builder.Configuration["UseFakeDevLogin"].Equals("true", StringComparison.CurrentCultureIgnoreCase))
{
    builder.Services.AddAuthentication("DevelopmentScheme")
    .AddScheme<AuthenticationSchemeOptions, DevelopmentAuthHandler>("DevelopmentScheme", null);
    builder.Services.AddAuthorization();
}
else
{
    /* https://manage.auth0.com/dashboard/us/dev-iy52fklqvibb5b8m/applications/f8isqivRoiCb2ZckyBchJMRzzHXLFtPN/quickstart/webapp/aspnet-core */
    builder.Services.AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    });
}


// Add DbContext HERE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

// everything below stays below
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseSession();




app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();