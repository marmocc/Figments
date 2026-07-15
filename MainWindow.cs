using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

namespace Figments;

public class MainWindow : Window
{
    private readonly TextBlock _titleLabel;

    public MainWindow()
    {
        Title = "Figments";
        Width = 500;
        Height = 300;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowDecorations = WindowDecorations.Full;
        Background = new SolidColorBrush(Branding.Background);

        _titleLabel = new TextBlock
        {
            Text = "Figments",
            FontFamily = Branding.LogoFont,
            FontSize = 96,
            FontWeight = FontWeight.Regular,
            Foreground = new SolidColorBrush(Branding.Foreground),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Padding = new Thickness(20),
        };

        Content = _titleLabel;
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
        base.OnClosing(e);
    }
}