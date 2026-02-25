using Alba;
using Alba.Security;
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