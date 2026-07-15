using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Figments;

public partial class App : Application
{
    private TrayIcon? _trayIcon;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var startingWindow = new MainWindow();
            desktop.MainWindow = startingWindow;
            SetupTrayIcon(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void SetupTrayIcon(IClassicDesktopStyleApplicationLifetime desktop)
    {
        var showItem = new NativeMenuItem("Show");
        showItem.Click += (s, e) => ShowMainWindow(desktop);

        var exitItem = new NativeMenuItem("Exit");
        exitItem.Click += (s, e) => desktop.Shutdown();

        _trayIcon = new TrayIcon
        {
            ToolTipText = "Figments",
            IsVisible = true,
            Menu = new NativeMenu
            {
                Items =
                {
                    showItem,
                    new NativeMenuItemSeparator(),
                    exitItem
                }
            }
        };

        _trayIcon.Clicked += (s, e) => ShowMainWindow(desktop);
    }

    private void ShowMainWindow(IClassicDesktopStyleApplicationLifetime desktop)
    {
        if (desktop.MainWindow == null) return;

        desktop.MainWindow.Show();
        desktop.MainWindow.WindowState = WindowState.Normal;
        desktop.MainWindow.Activate();
    }
}