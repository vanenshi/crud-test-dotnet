using Respawn;

namespace Presentation.FunctionalTest.Utils;

public static class RespawnerOptionsFactory
{
    public static RespawnerOptions GetRespawnerOptions()
    {
        return new RespawnerOptions()
        {
            SchemasToInclude = ["public"],
            TablesToIgnore =
            [
                // EF core migration history table
                "__EFMigrationsHistory"
            ],
            DbAdapter = DbAdapter.Postgres
        };
    }
}
