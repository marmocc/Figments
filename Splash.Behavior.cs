using System;
using Avalonia.Media;
using Avalonia.Threading;

namespace Figments;

// Behavior half of SplashWindow: purely the wave animation. Kept separate from
// SplashWindow.cs (layout/construction) so each file stays focused.
public partial class Splash
{
    // Purple <-> blue palette. Add/remove colors here to change the wave's character;
    // the animation code below interpolates smoothly between however many you provide.
    private static readonly Color[] Palette =
    [
        Color.Parse("#7B2FF7"), // violet
        Color.Parse("#4A00E0"), // indigo
        Color.Parse("#00C2FF"), // cyan-blue
        Color.Parse("#4A00E0"), // indigo (mirror, avoids a hard seam on loop)
    ];

    // Base offsets for the four gradient stops (evenly spaced across the text).
    private static readonly double[] StopOffsets = [0.00, 0.33, 0.66, 1.00];

    // How fast the wave travels. 1.0 = one full palette cycle per second.
    private const double CyclesPerSecond = 0.35;

    private DispatcherTimer? _timer;
    private double _phase; // 0..1, current position in the animation cycle

    private void InitializeAnimation()
    {
        Opened += (_, _) => StartAnimation();
        Closed += (_, _) => StopAnimation();
    }

    private void StartAnimation()
    {
        // Paint the first frame immediately so there's no blank flash before
        // the timer's first tick fires.
        ApplyGradientFrame();

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000.0 / 60.0) // ~60fps
        };
        _timer.Tick += OnTick;
        _timer.Start();
    }

    private void StopAnimation()
    {
        if (_timer is null) return;
        _timer.Stop();
        _timer.Tick -= OnTick;
        _timer = null;
    }

    private void OnTick(object? sender, EventArgs e)
    {
        _phase = (_phase + CyclesPerSecond / 60.0) % 1.0;
        ApplyGradientFrame();
    }

    private void ApplyGradientFrame()
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Avalonia.RelativePoint(0, 0.5, Avalonia.RelativeUnit.Relative),
            EndPoint = new Avalonia.RelativePoint(1, 0.5, Avalonia.RelativeUnit.Relative),
        };

        foreach (var baseOffset in StopOffsets)
        {
            brush.GradientStops.Add(new GradientStop(SampleAt(baseOffset + _phase), baseOffset));
        }

        _waveText.Foreground = brush;
    }

    private static Color SampleAt(double t)
    {
        t %= 1.0;
        if (t < 0) t += 1.0;

        var segmentCount = Palette.Length - 1;
        var scaled = t * segmentCount;
        var index = (int)scaled;
        if (index >= segmentCount) index = segmentCount - 1;

        var localT = scaled - index;
        return Lerp(Palette[index], Palette[index + 1], localT);
    }

    private static Color Lerp(Color a, Color b, double t)
    {
        byte L(byte x, byte y) => (byte)(x + (y - x) * t);
        return new Color(L(a.A, b.A), L(a.R, b.R), L(a.G, b.G), L(a.B, b.B));
    }
}
