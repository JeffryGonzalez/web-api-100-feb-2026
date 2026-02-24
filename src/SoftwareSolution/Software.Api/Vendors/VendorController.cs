using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Software.Api.Vendors;

[ApiController]
public class VendorController : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] CreateVendorRequestModel request)
    {
        if(request.Name.Trim().ToLower() == "oracle")
        {
            return BadRequest("We are not allowed to do business with them");
        }
        // if you are going to go across the network, touch the file system, database, whatever
        // YOU MUST USE ASYNC AND AWAIT.
        return Ok(request);
    }
}

public record CreateVendorRequestModel
{
    [MinLength(3), MaxLength(100)]
    public required string Name { get; set; }
   
    public required string Url { get; set; }
    public required VendorPointOfContactModel PointOfContact { get; set; } 
}

public record VendorPointOfContactModel
{
    [MinLength(3), MaxLength(100)]
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    
    public required string Phone { get; set; }
}
/*{
    "name": "Microsoft",
    "url": "https://www.microsoft.com",
    "pointOfContact": {
        "name": "Satya Nadella",
        "email": "satya@microsoft",
        "phone": "888 999-1212"
    }
}*/