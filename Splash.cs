using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Figments;

// Pure C# window definition -- no XAML. Layout/structure lives here;
// animation behavior lives in SplashWindow.Behavior.cs as a partial class.
public partial class Splash : Window
{
    // Kept as a field (not x:Name) so the behavior half of this partial class
    // can reach it directly -- no codegen involved at all.
    private readonly TextBlock _waveText;

    public Splash()
    {
        Title = "Figments";
        Width = 640;
        Height = 360;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        WindowDecorations = WindowDecorations.None;
        CanResize = false;
        Background = new SolidColorBrush(Color.Parse("#0D0D12"));
        TransparencyLevelHint = [WindowTransparencyLevel.Transparent];
        ShowInTaskbar = false;

        _waveText = new TextBlock
        {
            Text = "Figments",
            // Swap in your real font file's family name once you have it, e.g.
            // new FontFamily("avares://Figments/Assets/Fonts#Monoton")
            FontFamily = new FontFamily("avares://Figments/Assets/Fonts#VictorianParlorVintageAlternate"),
            FontSize = 72,
            FontWeight = FontWeight.Bold,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        Content = new Grid
        {
            Children = { _waveText }
        };

        InitializeAnimation();
    }
}
