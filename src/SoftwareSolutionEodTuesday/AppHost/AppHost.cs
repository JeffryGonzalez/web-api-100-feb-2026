using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var scalar = builder.AddScalarApiReference();
var pgServer = builder.AddPostgres("pg-server")
    .WithLifetime(ContainerLifetime.Persistent);

var softwareDb = pgServer.AddDatabase("software-db");

// Above this line is the "infra" stuff - or just development tools
// All the stuff that will need to be in the environment where this is running.
// After this line is the stuff I'm actually responsible for shipping.

var softwareApi = builder.AddProject<Projects.Software_Api>("software-api")
    .WithReference(softwareDb)
    .WaitFor(softwareDb);

scalar.WithApiReference(softwareApi);

builder.Build().Run();
