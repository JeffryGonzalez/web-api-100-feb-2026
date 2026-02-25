// using Alba;
// using Marten;
// using Microsoft.Extensions.DependencyInjection;
// using Software.Api.Vendors;
// using Software.Tests.Fixtures;


// namespace Software.Tests.Vendors;

// [Collection("SoftwareSystemTestCollection")]
// public class AddingAVendor(SoftwareSystemTestFixture fixture) : IClassFixture<SoftwareSystemTestFixture>
// {

//     [Fact]
//     public async Task CanAddVendor()
//     {
//         var vendorToPost = new CreateVendorRequestModel { Name = "Test Vendor", Url = "http://testvendor.com", PointOfContact = new VendorPointOfContactModel { Name = "John Doe", Email = "joe@aol.com", Phone="867-5309" } };
//         var response = await fixture.Host.Scenario(api =>
//         {
//             api.Post.Json(vendorToPost).ToUrl("/vendors");
//             api.StatusCodeShouldBeOk();
//         });


//         // We could check the database
//         using var scope = fixture.Host.Services.CreateScope();
//         var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();


//         var vendor = await session.Query<VendorEntity>().FirstOrDefaultAsync(v => v.Name == vendorToPost.Name, TestContext.Current.CancellationToken);

//         // Why would we do this? What would we look for?
//         // Another way?

//         Assert.NotNull(vendor);
//     }
// }
