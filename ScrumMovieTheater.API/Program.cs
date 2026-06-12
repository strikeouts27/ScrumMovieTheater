using ScrumMovieTheater.API.providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// this tells our api that our database has a context
//  an instance is created per HTTP request so they don't share instances.

// dependencies are services that help your project run. 
// dependency injection deals with internal services needed for the application.
// there can also be other services that deal with outside the project. For example db contexts. 
// builder.Services Add() methods are adding tools for dependency injection (internal assignments). 
// dependency injection is an service that provides/injects required dependencies/services when necessary.

// Scrum movie theater context connects to the database. 

// dependency, i will take care of anything you need as a dependency and i will create it and provide it for you
// // when you use it and are finished i will destroy it. 

// when we say builder.Services we are using the asp.net domain to create a tool for dependency injection
// you have to supply the blank class or code for the depenency injection to make its own tool. 
// ScrumMovieTheateContext is code we have to write as our blueprint for the tool for asp.net to use. 
// 

builder.Services.AddDbContext<ScrumMovieTheaterContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c=> c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scrum Movie Theater Api v1")); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// when it comes time to make controllers for the api, once you have created a controller this code will find that controller of the api. 
app.MapControllers();

app.Run();
