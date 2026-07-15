using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Figments;

public partial class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var splash = new Splash();
            splash.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }
}