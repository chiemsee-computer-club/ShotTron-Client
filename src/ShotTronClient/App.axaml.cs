using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Media;
using ShotTronClient.ViewModels;
using ShotTronClient.Views;

namespace ShotTronClient;

public class App : Application
{
    private static IClassicDesktopStyleApplicationLifetime? _desktopLifetime;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _desktopLifetime = desktop;

            desktop.MainWindow = new StartWindow
            {
                DataContext = new StartWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static void ChangeMainWindow(Window window)
    {
        if (_desktopLifetime != null)
        {
            Window? previousWindow = _desktopLifetime.MainWindow;
            _desktopLifetime.MainWindow = window;
            _desktopLifetime.MainWindow.Show();
            previousWindow?.Close();
        }
    }
        
    public static void TryEnableMicaEffect(Window window)
    {
            
        var themeManager = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();
        window.TransparencyBackgroundFallback = Brushes.Black;
        window.TransparencyLevelHint = WindowTransparencyLevel.Mica;
        window.ExtendClientAreaToDecorationsHint = true;

        // The background colors for the Mica brush are still based around SolidBackgroundFillColorBase resource
        // BUT since we can't control the actual Mica brush color, we have to use the window background to create
        // the same effect. However, we can't use SolidBackgroundFillColorBase directly since its opaque, and if
        // we set the opacity the color become lighter than we want. So we take the normal color, darken it and 
        // apply the opacity until we get the roughly the correct color
        // NOTE that the effect still doesn't look right, but it suffices. Ideally we need access to the Mica
        // CompositionBrush to properly change the color but I don't know if we can do that or not
        if (themeManager.RequestedTheme == FluentAvaloniaTheme.DarkModeString)
        {
            Color2 color = window.TryFindResource("SolidBackgroundFillColorBase", out object? value) ? (Color)value : new Color2(32, 32, 32);

            // color = color.LightenPercent(-0.9f);

            window.Background = new ImmutableSolidColorBrush(new Color2(0, 0, 0), 0.75);
        }
        else if (themeManager.RequestedTheme == FluentAvaloniaTheme.LightModeString)
        {
            // Similar effect here
            Color2 color = window.TryFindResource("SolidBackgroundFillColorBase", out object? value) ? (Color)value : new Color2(243, 243, 243);

            color = color.LightenPercent(0.5f);

            window.Background = new ImmutableSolidColorBrush(color, 0.9);
        }
    }
}