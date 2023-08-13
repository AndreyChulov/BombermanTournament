using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Resources;

public partial class TournamentServerFormDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(TournamentServerFormDataModel), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty IsButtonStartEnabledProperty = 
        DependencyProperty.Register(nameof(IsButtonStartEnabled), typeof(bool), 
            typeof(TournamentServerFormDataModel), new PropertyMetadata(default(bool)));

    public TournamentServerFormDataModel()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public bool IsButtonStartEnabled
    {
        get => (bool)GetValue(IsButtonStartEnabledProperty);
        set => SetValue(IsButtonStartEnabledProperty, value);
    }
}