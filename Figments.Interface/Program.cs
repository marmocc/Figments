namespace Figments.Interface;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Themes.Fluent;

class Program
{
    public static void Main(string[] args)
    {
        bool headless = args.Contains("--headless");
        if(headless) HeadlessMain(args);
        else AppBuilder.Configure<Application>().UsePlatformDetect().Start(AppMain, args);
    }

    public static void HeadlessMain(string[] args)
    {
        if(OperatingSystem.IsWindows())
            NativeConsole.Attach();
        Console.WriteLine("Hello, World!");
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new FluentTheme());
        var window = new Window
        {
            Title = "Figments",
            Width = 400,
            Height = 300,
            Content = new Label
            {
                Content = "Hello, World!",
                FontSize = 24,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            }
        };
        window.Show();
        app.Run(window);
    }
}