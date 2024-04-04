using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Presentation.FunctionalTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string envName = "Test";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.MigrateAsync().GetAwaiter().GetResult();

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(environment: envName);
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            configurationBuilder
                .AddJsonFile(path: "appsettings.Test.json", optional: false)
                .Build();
        });
    }
}
