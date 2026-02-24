// This  file is the "entry  point" of our API - it will be compiled to a class called Program
// and all the code below will be in the "static void Main(string[] args)" method - the "entry point" 
// of every .net "executable"


// this is saying "heya Microsoft, I don't know - give me stuff most people need to build an API.
using Demo.Api.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// everything above this line is configuration - "internal" stuff, "services" that we need
// to handle HTTP request, and make responses.
var app = builder.Build();
// After this, this is configuration of "pipeline" - which basically means
// How do we read actual requests and make responses.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // add an endpoint that you can "get" on your API (by default /openapi/v1.json)
}

//app.UseHttpsRedirection(); // if a request comes to this for http://... allow, but then send an HTTP redirect 302 

app.UseAuthorization();

app.MapControllers(); // Old Skool, OG way of creating a phone book for our receptionist.
// It uses "reflection" to inspect our code and create the "route table"


// Less Allocations.
app.MapGet("/status2", () =>
{
    var response = new StatusResponseModel { Message = "Ok from minimal land", WhenLastChecked = DateTime.Now, CheckedBy = "Jeff"};
    return response;
});

app.Run(); // This is a blocking method (do while(true) {....})
