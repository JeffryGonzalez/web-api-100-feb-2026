
using Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Software.Api.Clients;
using Software.Api.Vendors.Data;
using Software.Api.Vendors.Models;

namespace Software.Api.Vendors;

[ApiController]
public class VendorController(IDocumentSession session) : ControllerBase
{

    [HttpPost("/vendors")]
    [Authorize(Policy = "SoftwareCenterManager")] // unless you have a JWT from a trusted authority, you cannot call this endpoint.
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] CreateVendorRequestModel request,
        [FromServices] IDoNotifications api,
        [FromServices] TimeProvider clock
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
            Name = request.Url,
            Id = Guid.NewGuid(),
            PointOfContact = request.PointOfContact,
            Url = request.Url,
            CreatedAt = clock.GetUtcNow()

        };
        // save "it" to the database
        session.Store(entityToSave);
        // integrate services with remote procedure calls.
        // transactions cannot span database boundaries.
        // post the vendor another API AccountsPayable
        await api.SendNotification(new SoftwareShared.Notifications.NotificationRequest { Message = "New vendor added " + request.Name });
        await session.SaveChangesAsync(); // saved in the DB!

        // we have to senOk();
        // TODO: we should map this to a details model. (I'll show this in a few)
        return Created($"/vendors/{entityToSave.Id}",  entityToSave);
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

    [HttpPut("/vendors/{id:guid}/point-of-contact")]
    public async Task<ActionResult> UpdatePoc(Guid id, [FromBody] VendorPointOfContactModel request)
    {
        return Ok();
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
