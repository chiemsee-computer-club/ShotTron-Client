using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using MessageBox.Avalonia;
using ShotTronClient.ViewModels;

namespace ShotTronClient.Views;

public class StartWindow : ReactiveWindow<StartWindowViewModel>
{
    public StartWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        ExtendClientAreaChromeHints =
            ExtendClientAreaChromeHints.SystemChrome |
            ExtendClientAreaChromeHints.OSXThickTitleBar;

        App.TryEnableMicaEffect(this);
        
        LostFocus += delegate(object? sender, RoutedEventArgs args)
        {
            Background = Brushes.Black;
        };
        
        GotFocus += delegate(object? sender, GotFocusEventArgs args)
        {
            App.TryEnableMicaEffect(this);
        };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private bool isValid()
    {
        if (string.IsNullOrEmpty(ViewModel?.PlayerName))
        {
            MessageBoxManager
                .GetMessageBoxStandardWindow("Error", "Enter a valid Name")
                .ShowDialog(this);
            
            return false;
        }

        if (string.IsNullOrEmpty(ViewModel.SessionCode) || ViewModel.SessionCode.Length != 5)
        {
            MessageBoxManager
                .GetMessageBoxStandardWindow("Error", "Enter a valid game code to join")
                .ShowDialog(this);
            
            return false;
        }
        
        return true;
    }

    private void OnJoinSessionClicked(object? sender, RoutedEventArgs e)
    {
        Cursor = new Cursor(StandardCursorType.Wait);
        
        if (isValid())
        {
            App.ChangeMainWindow(new MainWindow
            {
                DataContext = new MainWindowViewModel()
            });
        }
        
        Cursor = Cursor.Default;
    }
}