using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ProductManagement.Forms;

namespace ProductManagement;

public partial class App : Application
{
    public static string ConnectionString { get; set; } = "";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow(ConnectionString);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
