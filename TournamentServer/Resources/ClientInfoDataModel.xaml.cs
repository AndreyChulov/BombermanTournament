using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Resources;

public partial class ClientInfoDataModel : UserControl
{
    public static readonly DependencyProperty ClientInfoProperty = 
        DependencyProperty.Register(nameof(ClientInfo), typeof(IConnectedClientInfo), 
            typeof(ClientInfoDataModel), new 
                PropertyMetadata(default(IConnectedClientInfo), ClientInfoProperty_Changed));

    public static readonly DependencyProperty TitleProperty = 
        DependencyProperty.Register(nameof(Title), typeof(string), 
            typeof(ClientInfoDataModel), new PropertyMetadata("Client info"));

    private static void ClientInfoProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IConnectedClientInfo connectedClientInfo = (IConnectedClientInfo)e.NewValue;
        
        ClientInfoDataModelHelper.InvokeUpdateActions(d.Dispatcher, d, connectedClientInfo);
    }

    public ClientInfoDataModel()
    {
        InitializeComponent();
        
        ClientInfoProperty_Changed(
            this, 
            new DependencyPropertyChangedEventArgs(ClientInfoProperty, null, 
                new ConnectedClientInfoStub()));
    }

    public IConnectedClientInfo ClientInfo
    {
        get => (IConnectedClientInfo)GetValue(ClientInfoProperty);
        set => SetValue(ClientInfoProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}