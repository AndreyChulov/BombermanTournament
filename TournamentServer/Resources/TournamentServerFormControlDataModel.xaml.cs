using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Resources;

public partial class TournamentServerFormControlDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(TournamentServerFormControlDataModel), 
            new PropertyMetadata(default(object), ServerProperty_Changed));

    public static readonly DependencyProperty IsButtonStartTournamentEnabledProperty = 
        DependencyProperty.Register(nameof(IsButtonStartTournamentEnabled), typeof(bool), 
            typeof(TournamentServerFormControlDataModel), new PropertyMetadata(default(bool)));

    public static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IServer server = (IServer)e.NewValue;
        
        TournamentServerFormControlDataModelHelper.InvokeUpdateActions(d.Dispatcher, d, server);
    }

    
    public TournamentServerFormControlDataModel()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public bool IsButtonStartTournamentEnabled
    {
        get => (bool)GetValue(IsButtonStartTournamentEnabledProperty);
        set => SetValue(IsButtonStartTournamentEnabledProperty, value);
    }
}