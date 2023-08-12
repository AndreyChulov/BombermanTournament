using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Resources;

public partial class ServerInfoDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), typeof(ServerInfoDataModel), 
            new PropertyMetadata(default(IServer), ServerProperty_Changed));

    public static readonly DependencyProperty IsServerStartedTextProperty = 
        DependencyProperty.Register(nameof(IsServerStartedText), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty StartStopServerButtonTextProperty = 
        DependencyProperty.Register(nameof(StartStopServerButtonText), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));


    private static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        bool isServerStarted = ((IServer)e.NewValue).IsServerStarted;
        
        d.SetValue(StartStopServerButtonTextProperty, GetStartStopServerButtonText(isServerStarted));
        d.SetValue(IsServerStartedTextProperty, GetServerStartedText(isServerStarted));
    }

    private static string GetStartStopServerButtonText(bool isServerStarted) 
        => isServerStarted ? "Stop server" : "Start server";

    private static string GetServerStartedText(bool isServerStarted) 
        => isServerStarted ? "Server started" : "Server stopped";
    
    public ServerInfoDataModel()
    {
        InitializeComponent();
        
        IsServerStartedText = GetServerStartedText(false);
        StartStopServerButtonText = GetStartStopServerButtonText(false);
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }
    
    public string IsServerStartedText
    {
        get => (string)GetValue(IsServerStartedTextProperty);
        init => SetValue(IsServerStartedTextProperty, value);
    }

    public string StartStopServerButtonText
    {
        get => (string)GetValue(StartStopServerButtonTextProperty);
        init => SetValue(StartStopServerButtonTextProperty, value);
    }
}