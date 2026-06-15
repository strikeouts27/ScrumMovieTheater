namespace ScrumMovieTheater.Services;

using ScrumMovieTheater.DTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

/*
 For this project to work you must right click on the solutions file and selecting properties. You need to tell visual studio to launch both the main project and the api.  
You can google what people are using for different things. 
 */
public class MovieDataService
{
    // a variable that stores this built in ASP.NET object of HttpClient for reference use because you can't just pull things out of thin air you have to specify parts of code sometimes. . 
    private readonly HttpClient _client;
    // this is a targeting instructions for firing information to an api for CreateClient. 
    private const string clientName = "ScrumMovieTheaterAPI";

    // a constructor was made to go get data. 
    // services empowers an object to speak with the api controller about getting data here. 
    // see above for object instantation and the materials needed for the create client function. 
    public MovieDataService(IHttpClientFactory factory)
    {
        // this creates a bridge  between the services and the api controller 
        // Create Client needs to know what string to use for the url. 
        // _client = new HttpClient (ClientName)
        // in this technique we create placeholder variables and than combine the parts togather to make a client. 
        // the purpose of a client is that it visit urls, sends http requests with the data that we hand it, and than it sends the user the response. 
        _client = factory.CreateClient(clientName);
    }
    // services acts as a bridge between the api and the controller. 

    public async Task<List<MovieDTO>> GetMoviesAsync()
    {
        // See the swagger get request executed. you should see a request url 
        // this code will say go into the request url and grab everything in the url that is before the slashes. 

        // using this connection point, go get me these events. it calls the events controller using the functonality of the getasync method. so see events controller.cs for the receiption point.
        // this line is calling code that we wrote in the api. 
        // this is where we are telling our empowered object to grab event data from the api controller.
        // going out to the api controller and waiting for information.
        // if the method is asking for information from something outside of the program that is a sign that you are using or creating a get request. This project and the API are in different programs. This is why we cannot import things from different programs. 
        // responses are not HTTP Request types. responses are the answers to the requests and the information contained to answer the requests information. 
        var response = await _client.GetAsync("movies");
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
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<MovieDTO>>(content);
    }

    // The controller called services with this converted dto. and the dto is passed in as a parameter. 
    public async Task CreateEventAsync(MovieDTO newEventDTO)
    {
        var jeventDTO = JsonConvert.SerializeObject(newEventDTO);
        // client was created earlier in the program see the top of the code page. 
        // THIS METHOD IS HTTP POST TO THE URL OF THE API. HERES THE ADDRESS AND FIRE THE INFORMATION. 
        // If the transmission is succesful it will transmit 200 or a success code. If not, an error message will happen. 
        var response = await _client.PostAsync("movies", new StringContent(jeventDTO, System.Text.Encoding.UTF8, "application/json"));
        // this is the checker that will see if the transmission was succesful. if not, an error exception will display. 
        response.EnsureSuccessStatusCode();

        // there is no return keyword here but there is an implicit return. 
    }

    // await -> I need to have to this information before we can do anything else. 
    public async Task UpdateEventAsync(MovieDTO eventDTO)
    {
        var jeventDTO = JsonConvert.SerializeObject(eventDTO);

        // await means this step of the process must be fully complete before the next code can be run.
        // if the await lock was not on this code than the code could continue running and not verfiy with the response.EnsureSccuessStatusCode(); 
        // PutAsync will fire off code on its own. So if the await lock was not put on there than the code would fire off and the ensure status code check would not run. 
        // look at await as if it were in a view of multiple methods working at once NOT from a standard way of viewing a single method where the programmer considers the order of operations being from the standpoint of reading from top to bottom. instead look at async different methods as running all at once.  you need to understand when steps need to be completed before other methods run. 
        // await should be used as a timing lock because asp.net will follow your instructions exactly and will run checks asap or instantley and the task that you give it takes more time than instant gratifiaction. 
        // there is also a multi request viewpoint/scope view for these things. for example if i wanted to run this method as non async and update 50 records than 50 records would need to be opened individually and process at two seconds each. 
        // but if i have async than 50 records can be opened at once and the operation can run all 50 times at the same time for each record and resolve everthing in just 2 seconds saving an increidble amount of time. 

        var response = await _client.PutAsync("movies", new StringContent(jeventDTO, System.Text.Encoding.UTF8, "application/json"));

        return; // this is return; is not normally input. but its just to help me see that nothing is returned. 
        // if this code comes back with an error code than tell me there is an error. if it comes back and says the response code is 200 or some other success code than say so. 
        // response.EnsureSuccessStatusCode();
        // services simply finishes everything that its going to do and other parts of asp.net continue to run services is finshed working. 
    }
}