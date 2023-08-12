using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Controls;

public partial class ServerInfo : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ServerInfo), new PropertyMetadata(default(IServer)));

    public static readonly DependencyProperty IsServerStartedTextProperty = 
        DependencyProperty.Register(nameof(IsServerStartedText), typeof(object), 
            typeof(ServerInfo), new PropertyMetadata(default(object)));

    public ServerInfo()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public object IsServerStartedText
    {
        get => (object)GetValue(IsServerStartedTextProperty);
        set => SetValue(IsServerStartedTextProperty, value);
    }
}