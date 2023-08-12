using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Controls;

public partial class ServerInfo : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), typeof(ServerInfo), 
            new PropertyMetadata(default(IServer)));

    public ServerInfo()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    private void StartStopServerButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Server.IsServerStarted)
        {
            Server.StopServer();
        }
        else
        {
            Server.StartServer();
        }
    }
}