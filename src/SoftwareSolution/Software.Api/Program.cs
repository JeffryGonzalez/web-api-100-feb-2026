var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults(); // this is using the Service Defaults project, setting up SRE etc.
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
