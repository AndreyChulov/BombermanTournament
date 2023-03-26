using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Controls;

public partial class ClientInfo : UserControl
{
    public static readonly DependencyProperty TitleProperty = 
        DependencyProperty.Register(nameof(Title), typeof(string), 
            typeof(ClientInfo), new PropertyMetadata(ClientInfoDesignContext.Title));

    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ClientInfo), new PropertyMetadata(default(IServer)));

    public static readonly DependencyProperty ClientIndexProperty = 
        DependencyProperty.Register(nameof(ClientIndex), typeof(int), 
            typeof(ClientInfo), new PropertyMetadata(default(int)));

    public ClientInfo()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public int ClientIndex
    {
        get => (int)GetValue(ClientIndexProperty);
        set => SetValue(ClientIndexProperty, value);
    }
}