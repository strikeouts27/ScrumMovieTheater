using ScrumMovieTheater.DTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;


/*
 For this project to work you must right click on the solutions file and selecting properties. You need to tell visual studio to launch both the main project and the api.  
You can google what people are using for different things. 
 */
namespace ScrumMovieTheater.Services; 

public class ShowroomDataService
    {
    // a variable that stores this built in ASP.NET object of HttpClient for reference use because you can't just pull things out of thin air you have to specify parts of code sometimes. . 
    private readonly HttpClient _client;

    // a constructor was made to go get data. 
    // services empowers an object to speak with the api controller about getting data here. 
    // see above for object instantation and the materials needed for the create client function.
    private const string clientName = "ScrumMovieTheaterAPI";

    public ShowroomDataService(IHttpClientFactory factory)
        {
            // this creates a bridge  between the services and the api controller 
            // Create Client needs to know what string to use for the url. 
            // _client = new HttpClient (ClientName)
            // in this technique we create placeholder variables and than combine the parts togather to make a client. 
            // the purpose of a client is that it visit urls, sends http requests with the data that we hand it, and than it sends the user the response. 

            _client = factory.CreateClient(clientName);
        }

    public async Task<List<ShowroomDTO>> GetShowroomsAsync()
    {

        // what is in the strings must match "" the controller name. 
        // var is whatever you need me to be as a data type, i will be for the requirements on the right side of the equal sign. 
        // keep a dividing line between comments 

        // response.EnsureSuccessStatusCode() 
        // this will take the response content and make it into a string. 
        // this will ensure that the response is successful, if not it will throw an error. FAIL FIRST 

        // keep a dividing line between comments 
        // this will take the response content and make it into a string. then it will deserialize the string into a list of EventDTO objects.
        // ASP.NET specifies that json will be the format that is transmitted.  
        // upon success the event information is translated into a string which is than stored in a format fitting for the next method to package it into a DTO. 

        // keep a dividing line between code. 
        // desearlize think convert from string to object. it will assign all of the proprties of json into the corresponding fields by name.
        // this will return a list of events. 
        // .JsonSerializer is microsofts json convertor built by microsoft. joseph did NOT reccomend using it. .JsonSerializer 
        //return System.Text.Json.JsonSerializer.Deserialize<List<EventDTO>>(content);
        // deserialize and pack to DTO
        var response = await _client.GetAsync("showroom");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShowroomDTO>>(content); 
    }

    // we do not need to use route tags when the method is in the same project. we import instead. 
    // if what is calling it is outside of the program, than you use HTTP methods. 
    public async Task CreateShowroomAsync(ShowroomDTO showroomDTO)
    {
        var jshowroomDTO = JsonConvert.SerializeObject(showroomDTO);
        var response = await _client.PostAsync("showroom", new StringContent(
            jshowroomDTO, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        return;
    }

    public async Task UpdateEventAsync(ShowroomDTO showroomDTO)
    {
        var jshowroomDTO = JsonConvert.SerializeObject(showroomDTO);
        var response = await _client.PutAsync("showroom", new StringContent(jshowroomDTO, System.Text.Encoding.UTF8, "application/json")); 
    }
}



