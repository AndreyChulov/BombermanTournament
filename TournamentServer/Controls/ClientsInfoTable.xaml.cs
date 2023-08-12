using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;

namespace TournamentServer.Controls;

public partial class ClientsInfoTable : UserControl
{
    public static readonly DependencyProperty ServerProperty = DependencyProperty.Register(nameof(Server), 
        typeof(IServer), typeof(ClientsInfoTable), 
        new PropertyMetadata(default(IServer)));

    public ClientsInfoTable()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }
}