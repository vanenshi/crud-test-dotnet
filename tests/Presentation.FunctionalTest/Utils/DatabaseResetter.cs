using System.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Respawn;

namespace Presentation.FunctionalTest.Utils;

public class DatabaseResetter
{
    private readonly ApplicationDbContext _applicationDbContext;
    private Respawner? _respawner;

    public DatabaseResetter(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task InitializeAsync()
    {
        var connection = _applicationDbContext.Database.GetDbConnection();

        if (connection.State is ConnectionState.Closed or ConnectionState.Broken)
        {
            await connection.OpenAsync();
        }

        _respawner = await Respawner.CreateAsync(
            connection: connection,
            options: RespawnerOptionsFactory.GetRespawnerOptions()
        );
    }

    public async Task ResetDatabase()
    {
        if (_respawner == null)
            await InitializeAsync();

        var connection = _applicationDbContext.Database.GetDbConnection();
        if (connection.State is ConnectionState.Closed or ConnectionState.Broken)
        {
            await connection.OpenAsync();
        }

        await _respawner!.ResetAsync(connection: connection);
    }
}
