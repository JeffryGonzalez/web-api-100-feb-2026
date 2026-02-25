//using Alba;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Software.Tests.Vendors;

//public class GettingAllVendors
//{

//    [Fact]
//    public async Task CanGetVendorList()
//    {
//        // Given I have a running API at some address (checjk!)
//        // When I send an Http GET request to the /vendors resource
//        // then I should get.... some vendors?
//        var host = await AlbaHost.For<Program>(); // start up the API using our Program cs.

//        await host.Scenario(api =>
//        {
//            api.Get.Url("/vendors");
//            api.StatusCodeShouldBeOk();

//        });

//    }
//}
