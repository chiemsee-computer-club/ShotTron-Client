using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace ShotTronClient.Views;

public class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        // App.TryEnableMicaEffect(this);
        
        ExtendClientAreaChromeHints =
            ExtendClientAreaChromeHints.SystemChrome |
            ExtendClientAreaChromeHints.OSXThickTitleBar;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnSnoozeClicked(object? sender, RoutedEventArgs e)
    {
    }
}