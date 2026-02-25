using ImTools;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Software.Api.Clients;
using System.ComponentModel.DataAnnotations;

namespace Software.Api.Vendors;

[ApiController]
public class VendorController(IDocumentSession session) : ControllerBase
{
    //private IDocumentSession _session;

    //public VendorController(IDocumentSession session)
    //{
    //    _session = session;
    //}
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] CreateVendorRequestModel request,
        [FromServices] NotificationsApi api
 
  
        )
    {
        if(request.Name.Trim().ToLower() == "oracle")
        {
            return BadRequest("We are not allowed to do business with them");
        }
        // if you are going to go across the network, touch the file system, database, whatever
        // YOU MUST USE ASYNC AND AWAIT.
        // 
        var entityToSave = new VendorEntity
        {
            Name = request.Name,
            Id = Guid.NewGuid(),
            PointOfContact = request.PointOfContact,
            Url = request.Url,
            CreatedAt  = DateTimeOffset.UtcNow

        };
        // save "it" to the database
        session.Store(entityToSave);
        // integrate services with remote procedure calls.
        // transactions cannot span database boundaries.
        // post the vendor another API AccountsPayable
        await api.SendNotification(new SoftwareShared.Notifications.NotificationRequest { Message = "Just letting you know..." });
        await session.SaveChangesAsync(); // saved in the DB!

        // we have to send a POST to the notification API to let, uh, someone know that a new vendor was added.
        return Ok();
    }

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetAllVendorsAsync(CancellationToken token)
    {
        var allVendors = await session.Query<VendorEntity>()
            .Select(v => new VendorSummaryModel
            {
                Id = v.Id,
                Name  = v.Name,
                Url = v.Url
            })
            .ToListAsync(token);
        return Ok(allVendors);
    }

    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id)
    {
        var vendor = await session.Query<VendorEntity>()
           .Where(v => v.Id == id)
             .Select(v => new VendorDetailsModel
             {
                 Id = v.Id,
                 Name = v.Name,
                 Url = v.Url,
                 PointOfContact = v.PointOfContact
             })
            .SingleOrDefaultAsync();

        if(vendor is null)
        {
            return NotFound(); // 404
        }
        else
        {
            return Ok(vendor);
        }
    }
}

public class VendorEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required string Url { get; set; }
    public required VendorPointOfContactModel PointOfContact { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

public record VendorDetailsModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required string Url { get; set; }
    public required VendorPointOfContactModel PointOfContact { get; set; }

}

public record VendorSummaryModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required string Url { get; set; }
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