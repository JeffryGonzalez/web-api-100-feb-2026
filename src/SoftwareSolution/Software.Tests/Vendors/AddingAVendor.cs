using Alba;
using Software.Api.Vendors;
using Software.Tests.Fixtures;


namespace Software.Tests.Vendors;

[Collection("SoftwareSystemTestCollection")]
public class AddingAVendor(SoftwareSystemTestFixture fixture) : IClassFixture<SoftwareSystemTestFixture>
{

    [Fact]
    public async Task CanAddVendor()
    {
        var vendorToPost = new CreateVendorRequestModel { Name = "Test Vendor", Url = "http://testvendor.com", PointOfContact = new VendorPointOfContactModel { Name = "John Doe", Email = "joe@aol.com", Phone="867-5309" } };
        var response = await fixture.Host.Scenario(api =>
        {
            api.Post.Json(vendorToPost).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });
        
    }
}
