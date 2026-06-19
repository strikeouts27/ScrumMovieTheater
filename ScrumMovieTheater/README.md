

Overview

Hello the scrum movie theater project was created to showcase the skills of the students enrolled in 2026SU-INEW-2332-1 Comp SW Proj; Code,Test and Scrum Practice. 
This project will showcase the following skills. We made this project so that we could show employers that we understand skills that they may be looking for in their next hired developer/ intern.

The development team and their githubs. 

Andrew Stribling (strikeouts27) https://github.com/strikeouts27?tab=repositories
Ibrahim Khan (topibrahimkhan-dev) https://github.com/topibrahimkhan-dev?tab=repositories
EugeneNtaganira https://github.com/EugeneNtaganira?tab=repositories
Salma (queen145) https://github.com/queen145?tab=repositories
Bry (bdenne2) https://github.com/bdenne2?tab=repositories -> Bry was a tutor hired by the group. 

- HTML, CSS, Javascript 
- MySQl, Swagger, Asp.Net MVC
- Git, Github
- Scrumwise 

Here is a link to the Github Repository that has the code. 
https://github.com/strikeouts27/ScrumMovieTheater


# Scrum Movie Theater

This README adds setup and run instructions so you can get the project working locally.

How to use (step-by-step)
To access this project please download the following items. 

Visual Studio 2026: https://visualstudio.microsoft.com/downloads/

1. Download and Use these Extensions -> C# Dev Kit 

MySql: https://dev.mysql.com/downloads/workbench/ 
MySql Workbench: https://www.mysql.com/products/workbench/


Go to Github.com at this link 
2.	Clone and prepare
git clone https://github.com/strikeouts27/ScrumMovieTheater.git
cd ScrumMovieTheater
dotnet restore
dotnet build

3.	Configure the database connection (TO DO) 

•	Edit ScrumMovieTheater.API/appsettings.Development.json (or create it) and add a connection string: ConnectionStrings.DefaultConnection = "server=localhost;port=3306;database=ScrumMovieTheater;uid=<user>;password=<password>"
•	Recommended: remove hard-coded string in Providers/ScrumMovieTheaterContext.cs and register DbContext in Program.cs: builder.Services.AddDbContext<ScrumMovieTheaterContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

3.	(Optional) Scaffold or apply migrations
•	Reverse-engineer existing DB: dotnet ef dbcontext scaffold "server=localhost;port=3306;database=ScrumMovieTheater;uid=<user>;password=<password>" MySql.EntityFrameworkCore --output-dir Models --context-dir Providers --force
•	Or run migrations (if present): dotnet ef database update --project .\ScrumMovieTheater.API\ScrumMovieTheater.API.csproj

4.	Trust dev HTTPS certificate (if using HTTPS profile)
•	dotnet dev-certs https --trust
5.	Run the API
 1. 
•	From command line: dotnet run --project .\ScrumMovieTheater.API\ScrumMovieTheater.API.csproj (API listens on ports from ScrumMovieTheater.API/Properties/launchSettings.json — typically http://localhost:5013 and https://localhost:7095)
•	Or open solution in Visual Studio and set ScrumMovieTheater.API as startup project and choose desired launch profile.
6.	Run the frontend (Razor Pages)

•	dotnet run --project .\ScrumMovieTheater\ScrumMovieTheater.csproj
•	Open the frontend URL (check its launchSettings; typically http://localhost:5000)

7.	Open Swagger and API endpoints

•	Swagger UI (when running API in Development):
•	HTTP profile: http://localhost:5013/swagger/index.html
•	HTTPS profile: https://localhost:7095/swagger/index.html
•	Raw OpenAPI JSON: http://localhost:5013/swagger/v1/swagger.json

Quick troubleshooting
•	404 on /swagger: ensure ScrumMovieTheater.API is running, ASPNETCORE_ENVIRONMENT=Development, Program.cs registers AddEndpointsApiExplorer/AddSwaggerGen and calls app.UseSwagger()/UseSwaggerUI().
•	HTTPS errors: trust dev cert, or use the HTTP profile URL.
•	DB errors: confirm MySQL is running, credentials/port are correct, and provider version matches EF Core version.
•	Port conflicts: change launchSettings ports or stop the process using the port.

Developer Notes 

### Look for launch settings on both. 
API
    ("http://localhost:5013/api")

Main Project 
http://localhost:5000

Swagger URL 
https://localhost:7095/swagger/index.html

This scaffold command helps generate the my sql tables you will need. 

dotnet ef dbcontext scaffold "server=localhost;port=3306;database=ScrumMovieTheater;uid=strikeouts27;password=Baseball100!!" MySql.EntityFrameworkCore --output-dir Models --context-dir Providers --force

---