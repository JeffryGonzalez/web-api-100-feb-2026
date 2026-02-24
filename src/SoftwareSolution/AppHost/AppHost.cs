var builder = DistributedApplication.CreateBuilder(args);



// All the stuff that will need to be in the environment where this is running.

var softwareApi = builder.AddProject<Projects.Software_Api>("software-api");

builder.Build().Run();
