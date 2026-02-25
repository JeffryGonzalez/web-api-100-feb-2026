using Alba;
using Alba.Security;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Clients;
using SoftwareShared.Notifications;
using Testcontainers.PostgreSql;

namespace Software.Tests.Fixtures;

public class SoftwareSystemTestFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; set; } = null!;
    private PostgreSqlContainer _pgContainer = null!;

    public async ValueTask InitializeAsync()
    {
        _pgContainer = new PostgreSqlBuilder("postgres:17.6")
            .Build(); 
        await _pgContainer.StartAsync();
        Host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("ConnectionStrings:software-db", _pgContainer.GetConnectionString());
            // config.ConfigureServices = add a service that doesn't exist yet.
            // config.ConfigureTestServices = replace one that is there with something else for this test.
            config.ConfigureTestServices(sp =>
            {
                sp.AddScoped<IDoNotifications, DummyNotifier>();
            });
        }, new AuthenticationStub().WithName("test-user") );
       
    }
    public async ValueTask DisposeAsync()
    {
        await Host.DisposeAsync();
        await _pgContainer.DisposeAsync();
    }
}

[CollectionDefinition("SoftwareSystemTestCollection")]
public class SystemTestCollection : ICollectionFixture<SoftwareSystemTestFixture>
{
    
}

public class DummyNotifier : IDoNotifications
{
    public Task SendNotification(NotificationRequest request)
    {
        // cool cool. Whatevs, dude.
        return Task.CompletedTask;
    }
}