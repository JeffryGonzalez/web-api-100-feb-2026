using JasperFx.CommandLine.Descriptions;
using Marten;
using Software.Api.CatalogItems;
using Software.Api.Clients;


var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDataSource("software-db"); // this is using the NpgsqlDataSource project, which is a wrapper around the Npgsql library, and it is used to create a connection pool for the database.
builder.Services.AddValidation(); // [ApiController]
builder.AddServiceDefaults(); // this is using the Service Defaults project, setting up SRE etc.
// Add services to the container.
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System); // this is a new feature in .NET 8, and it is used to abstract away the system clock, so we can use a different clock in our tests.

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorizationBuilder().AddPolicy("SoftwareCenterManager", pol =>
{
    pol.RequireRole("SoftwareCenter");
    pol.RequireRole("Manager");
}).AddPolicy("SoftwareCenter", pol =>
{
    pol.RequireRole("SoftwareCenter");
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("software-db") ??
    throw new Exception("no connection string");

// this next like will look like this, but be slightly different for whatever specific database you are connection to.

// scoped services are services you use in your controllers, endpoints, etc.
// It is created "brand new" for each http request, and automatically "disposed" at the end of the request.

//builder.Services.AddScoped<IDocumentSession, SomeInternalConcreteClass>();
//builder.Services.AddSingleton<NpgSqlConnectionManager>();

builder.Services.AddMarten(options =>
{


}).UseLightweightSessions().UseNpgsqlDataSource();

// look at the environment variable called ASPNETCORE_ENVIRONMENT. If it is there, and has a value,
// look in appsettings.ENVIRONMENT.json

// appsettings.json
// ConnectionString__software-db

// The last place it looks is Environment Variables.
builder.Services.AddHttpClient<NotificationsApi>(client =>
{
    // we prefer https, but we'll http if that is availble, and get the address for that api.
    client.BaseAddress = new Uri("https+http://notification-api");
});

builder.Services.AddScoped<IDoNotifications>(sp =>
{
    return sp.GetRequiredService<NotificationsApi>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapCatalogItemRoutes();

//app.MapPost("/ma/vendors", (CreateVendorRequestModel request) =>
//{
//    return TypedResults.Ok();
//});
app.MapDefaultEndpoints(); // this comes from service defaults, and this is mostly health checks.
app.Run();
