using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ScrumMovieTheater.API.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// this code works with the asp.net secrets manager. it can store information from the connection string in app settings.

var conStrBuilder = new MySqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("DefaultConnection"));
if (builder.Configuration["DefaultPassword"] == null)
{
    throw new Exception("Please create password secret. See the README for instructions.");
}
conStrBuilder.Password = builder.Configuration["DefaultPassword"]; // this is a secret see readme
// this line of code generates the attributes of the mysqlconnection string builder class. 
var connection = conStrBuilder.ConnectionString;

// Add DbContext HERE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connection,
        ServerVersion.AutoDetect(
            connection)
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
