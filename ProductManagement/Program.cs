using Avalonia;
using System;
using DotNetEnv;
using ProductManagement.Scripts.Db.Migrations;
namespace ProductManagement;

static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {
        Env.Load();

        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var db_username = Environment.GetEnvironmentVariable("DB_USERNAME");
        var db_password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connection = $"Host={host};Port={port};Database={dbName};Username={db_username};Password={db_password}";

        DatabaseMigrater.Migrate(connection);

        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
