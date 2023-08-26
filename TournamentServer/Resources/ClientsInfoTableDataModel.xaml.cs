using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Resources;

public partial class ClientsInfoTableDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ClientsInfoTableDataModel), new PropertyMetadata(default(IServer), 
                ServerProperty_Changed));

    public static readonly DependencyProperty ClientsInfoProperty = 
        DependencyProperty.Register(nameof(ClientsInfo), typeof(ConnectedClientInfoArray), 
            typeof(ClientsInfoTableDataModel), 
            new PropertyMetadata(default(ConnectedClientInfoArray)));

    private static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IServer server = (IServer)e.NewValue;
        
        ClientsInfoTableDataModelHelper.InvokeUpdateActions(d.Dispatcher, d, server);
    }

    public ClientsInfoTableDataModel()
    {
        InitializeComponent();
        
        ServerProperty_Changed(
            this, 
            new DependencyPropertyChangedEventArgs(ServerProperty, null, new ServerStub()));
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public ConnectedClientInfoArray ClientsInfo
    {
        get => (ConnectedClientInfoArray)GetValue(ClientsInfoProperty);
        set => SetValue(ClientsInfoProperty, value);
    }
}