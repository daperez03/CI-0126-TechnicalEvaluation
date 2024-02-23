namespace TechnicalEvaluation.Infrastructure.Tests.IntegrationTests;

public record IntegrationTestSettings
{
    public const string connectionString =  
        "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=TechnicalEvaluation.Database;Integrated Security=true;";
}
