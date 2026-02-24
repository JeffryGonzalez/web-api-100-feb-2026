using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controller;

// 
[ApiController]
public class StatusController : ControllerBase
{

    // don't do this.
    // GET ..../status

    [HttpGet("/status")] // This is using "reflection"
    public StatusResponseModel  GetTheStatusAsync()
    {
        // call into an api, look up in a database, this is fake.
        var response = new StatusResponseModel
        {
            Message = "Still Looks Good!",
            WhenLastChecked = DateTime.Now,

        };
        return response;
    }
}


public record StatusResponseModel
{
    public required string Message { get; init; } = "";
    public required DateTimeOffset WhenLastChecked { get; init; }

    public string? CheckedBy { get; set; }

}