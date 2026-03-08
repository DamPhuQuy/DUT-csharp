using DbUp;
using System;
using System.Reflection;

namespace ProductManagement.Scripts.Db.Migrations;

public static class DatabaseMigrater
{
    public static void Migrate(string connection)
    {
        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        Console.WriteLine("Checking database connection...");

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Database migration failed: {connection}");
            Console.WriteLine(result.Error);
            throw result.Error;
        }

        Console.WriteLine("Connecting with: {connection}");
    }
}
