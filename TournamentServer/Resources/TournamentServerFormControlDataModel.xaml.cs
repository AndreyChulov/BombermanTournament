using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Resources;

public partial class TournamentServerFormControlDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(TournamentServerFormControlDataModel), 
            new PropertyMetadata(default(object), ServerProperty_Changed));

    public static readonly DependencyProperty IsButtonStartEnabledProperty = 
        DependencyProperty.Register(nameof(IsButtonStartEnabled), typeof(bool), 
            typeof(TournamentServerFormControlDataModel), new PropertyMetadata(default(bool)));

    public static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IServer server = (IServer)e.NewValue;
        bool isServerProcessingCommand = server.IsServerProcessingCommand;
        
        //d.SetValue(IsButtonStartEnabledProperty, !isServerProcessingCommand);
        //d.SetValue(IsServerStartedTextProperty, GetServerStartedText(isServerStarted));
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

    public bool IsButtonStartEnabled
    {
        get => (bool)GetValue(IsButtonStartEnabledProperty);
        set => SetValue(IsButtonStartEnabledProperty, value);
    }
}