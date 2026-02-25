using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults(); // this is using the Service Defaults project, setting up SRE etc.
// Add services to the container.

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
    options.Connection(connectionString);

}).UseLightweightSessions();

// look at the environment variable called ASPNETCORE_ENVIRONMENT. If it is there, and has a value,
// look in appsettings.ENVIRONMENT.json

// appsettings.json
// ConnectionString__software-db

// The last place it looks is Environment Variables.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultEndpoints(); // this comes from service defaults, and this is mostly health checks.
app.Run();
