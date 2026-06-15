namespace ScrumMovieTheater.Services;

using ScrumMovieTheater.DTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization; 

public class TheaterDataService
{
    // a variable that stores this built in ASP.NET object of HttpClient for reference use because you can't just pull things out of thin air you have to specify parts of code sometimes. ctrl e ctrl w is the command. or search and look at feature settings word wrap
    private readonly HttpClient _client;

    private const string clientName = "ScrumMovieTheaterAPI";

    public TheaterDataService(IHttpClientFactory factory)
    {
        // this creates a bridge  between the services and the api controller 
        // Create Client needs to know what string to use for the url. 
        // _client = new HttpClient (ClientName)
        // in this technique we create placeholder variables and than combine the parts togather to make a client. 
        // the purpose of a client is that it visit urls, sends http requests with the data that we hand it, and than it sends the user the response. 

        _client = factory.CreateClient(clientName); 
    }

    // services acts as a bridge between the api and the controller 

    public async Task<List<TheaterDTO>> GetTheatersAsync()
    {
        // what is in the strings must match "" the controller name.
        var response = await _client.GetAsync("theaters");
        // this will ensure that the response is successful, if not it will throw an error. FAIL FAST
        response.EnsureSuccessStatusCode();
        // this will take the response content and make it into a string. then it will deserialize the string into a list of EventDTO objects.
        // ASP.NET specifies that json will be the format that is transmitted.  
        // upon success the event information is translated into a string which is than stored in a format fitting for the next method to package it into a DTO. 
        var content = await response.Content.ReadAsStringAsync();

        // desearlize think convert from string to object. it will assign all of the proprties of json into the corresponding fields by name.
        // this will return a list of events. 

        // .JsonSerializer is microsofts json convertor built by microsoft. joseph did NOT reccomend using it. .JsonSerializer 
        //return System.Text.Json.JsonSerializer.Deserialize<List<EventDTO>>(content);
        // deserialize and pack to DTO
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TheaterDTO>>(content);
    }

    // The controller called services with this converted dto. and the dto is passed in as a parameter. 

    public async Task CreateTheaterAsync(TheaterDTO newTheaterDTO)
    {
        var jtheaterDTO = JsonConvert.SerializeObject(newTheaterDTO);

        var response = await _client.PostAsync("theaters", new StringContent(
        jtheaterDTO, System.Text.Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        return;
        // client was created earlier in the program see the top of the code page. 
        // THIS METHOD IS HTTP POST TO THE URL OF THE API. HERES THE ADDRESS AND FIRE THE INFORMATION. 
        // If the transmission is succesful it will transmit 200 or a success code. If not, an error message will happen.
        // there is no return keyword here but there is an implicit return. you can leave a return if you hate implicit code. which i do! 
    }

    // why do we need these methods to be async? well
    // 1 multi tasking is effective. 
    // 2. if you use one thing that needs to be async everything connected to it requires async. 
    // 3. please see my comments in movie data service regarding why we are doing what we are doing for these methods. 
    public async Task UpdateEventAsync(TheaterDTO theaterDTO) {
        var jtheaterDTO = JsonConvert.SerializeObject(theaterDTO);
        var response = await _client.PutAsync("theaters", new StringContent(jtheaterDTO, System.Text.Encoding.UTF8, "application/json"));
        return;
    }

}

