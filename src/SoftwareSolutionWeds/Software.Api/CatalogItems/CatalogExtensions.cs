using Software.Api.CatalogItems.Operations;
using Software.Api.Vendors.Models;

namespace Software.Api.CatalogItems;

public static class CatalogExtensions
{

   extension(IEndpointRouteBuilder builder)
    {
        public IEndpointRouteBuilder MapCatalogItemRoutes()
        {
            var group = builder.MapGroup("/vendors").RequireAuthorization("SoftwareCenter").WithDescription("Use this to add catalog items to the vendors");


            group.MapPost("/v2", (CreateVendorRequestModel model) =>
            {
                return TypedResults.Ok();
            });
            group.MapPost("/{vendorId:guid}", AddCatalogItem.AddCatalogItemAsync);
            
            return builder;
        }
    }
}
