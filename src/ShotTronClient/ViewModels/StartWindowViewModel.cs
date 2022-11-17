using System.Runtime.Serialization;
using ReactiveUI;

namespace ShotTronClient.ViewModels;

public class StartWindowViewModel : ViewModelBase
{
    private string? _sessionCode;
    private string? _playerName;

    public string? SessionCode
    {
        get => _sessionCode;
        set => this.RaiseAndSetIfChanged(ref _sessionCode, value);
    }

    [DataMember]
    public string? PlayerName
    {
        get => _playerName;
        set => this.RaiseAndSetIfChanged(ref _playerName, value);
    }
}