### Look for launch settings on both. 
API
    ("http://localhost:5013/api")

Main Project 
http://localhost:5000

Swagger URL 
https://localhost:7095/swagger/index.html

This scaffold command helps generate the my sql tables you will need. 

dotnet ef dbcontext scaffold "server=localhost;port=3306;database=ScrumMovieTheater;uid=strikeouts27;password=Baseball100!!" MySql.EntityFrameworkCore --output-dir Models --context-dir Providers --force