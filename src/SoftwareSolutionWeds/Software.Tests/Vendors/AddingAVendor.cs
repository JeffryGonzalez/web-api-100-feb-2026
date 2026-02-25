using Alba;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Software.Api.Vendors.Data;
using Software.Api.Vendors.Models;
using Software.Tests.Fixtures;
using SoftwareShared.Notifications;


namespace Software.Tests.Vendors;

// any test classes we have that have this same attribute will use the same instance of the API and database.
// If we need "clean" database, create a different collection.
[Collection("SoftwareSystemTestCollection")]
public class AddingAVendor(SoftwareSystemTestFixture fixture) : IClassFixture<SoftwareSystemTestFixture>
{

    [Fact]
    public async Task CanAddVendor()
    {
        var vendorToPost = new CreateVendorRequestModel { 
            Name = "Test Vendor", 
            Url = "http://testvendor.com", 
            PointOfContact = new VendorPointOfContactModel { Name = "John Doe", Email = "joe@aol.com", Phone = "867-5309" } 
        };
        var response = await fixture.Host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });


        // We could check the database
        using var scope = fixture.Host.Services.CreateScope();
        var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();


        var vendor = await session.Query<VendorEntity>().FirstOrDefaultAsync(v => v.Name == vendorToPost.Name, TestContext.Current.CancellationToken);
        Assert.NotNull(vendor);
        // TODO: Check the created at property.

        // TODO: Bryce - what do you want?
        await fixture.NotificationMock.Received().SendNotification(Arg.Is<NotificationRequest>(n => n.Message.Contains("New vendor added") && n.Message.Contains(vendorToPost.Name)));

        // Todo: What is the "full" scenario?

    }
}
