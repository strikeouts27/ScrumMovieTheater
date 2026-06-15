namespace ScrumMovieTheater.Services;

using ScrumMovieTheater.DTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

/*
 For this project to work you must right click on the solutions file and selecting properties. You need to tell visual studio to launch both the main project and the api.  
You can google what people are using for different things. 
 */

public class ShowroomDataService
{
    // a variable that stores this built in ASP.NET object of HttpClient for reference use because you can't just pull things out of thin air you have to specify parts of code sometimes. . 
    private readonly HttpClient _client;

    // a constructor was made to go get data. 
    // services empowers an object to speak with the api controller about getting data here. 
    // see above for object instantation and the materials needed for the create client function.
    private const string clientName = "ScrumMovieShowtimesAPI";

    public ShowroomDataService(IHttpClientFactory factory)
    {
        // this creates a bridge  between the services and the api controller 
        // Create Client needs to know what string to use for the url. 
        // _client = new HttpClient (ClientName)
        // in this technique we create placeholder variables and than combine the parts togather to make a client. 
        // the purpose of a client is that it visit urls, sends http requests with the data that we hand it, and than it sends the user the response. 

        _client = factory.CreateClient(clientName);
    }

    public async Task<List<ShowroomDTO>> GetShowtimesAsync() { }

}
