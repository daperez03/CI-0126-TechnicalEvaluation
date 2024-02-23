

using Microsoft.EntityFrameworkCore;

namespace TechnicalEvaluation.Infrastructure.Tests.IntegrationTests;

public class DatabaseSetup
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public static string GetScript(string sqlFileName)
    {
        var ScriptPath = Directory.GetParent(Directory.GetCurrentDirectory())
            .Parent
            .Parent
            + "\\IntegrationTests\\SqlScripts\\" + sqlFileName;

        var script = File.ReadAllText(ScriptPath);
        return script;
    }

    public static void TestDatabaseFixture(string sqlScript)
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                _databaseInitialized = true;
            }

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>();

            using (var context = new ApplicationDbContext(DbOptions.UseSqlServer(IntegrationTestSettings.connectionString).Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.ExecuteSqlRaw(sqlScript);
            }

        }
    }
}
