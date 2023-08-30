using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Resources;

public partial class ClientInfoDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ClientInfoDataModel), new 
                PropertyMetadata(default(IServer), ServerProperty_Changed));

    public static readonly DependencyProperty TitleProperty = 
        DependencyProperty.Register(nameof(Title), typeof(string), 
            typeof(ClientInfoDataModel), new PropertyMetadata("Client info"));

    public static readonly DependencyProperty ClientIPProperty = 
        DependencyProperty.Register(nameof(ClientIP), typeof(string), 
            typeof(ClientInfoDataModel), new PropertyMetadata(default(string)));
    
    public static readonly DependencyProperty ClientPortProperty = 
        DependencyProperty.Register(nameof(ClientPort), typeof(int), 
            typeof(ClientInfoDataModel), new PropertyMetadata(default(int)));

    public static readonly DependencyProperty ClientIndexProperty = 
        DependencyProperty.Register(nameof(ClientIndex), typeof(int), 
            typeof(ClientInfoDataModel), new PropertyMetadata(default(int)));

    private static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IServer server = (IServer)e.NewValue;
        ClientInfoDataModel clientInfoDataModel = (ClientInfoDataModel)d;
        
        ClientInfoDataModelHelper.InvokeUpdateActions(d.Dispatcher, clientInfoDataModel, server);
    }

    public ClientInfoDataModel()
    {
        InitializeComponent();

        ClientIndex = 0;
        ServerProperty_Changed(
            this, 
            new DependencyPropertyChangedEventArgs(ServerProperty, null, 
                new ServerStub()));
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string ClientIP
    {
        get => (string)GetValue(ClientIPProperty);
        set => SetValue(ClientIPProperty, value);
    }

    public int ClientPort
    {
        get => (int)GetValue(ClientPortProperty);
        set => SetValue(ClientPortProperty, value);
    }

    public int ClientIndex
    {
        get => (int)GetValue(ClientIndexProperty);
        set => SetValue(ClientIndexProperty, value);
    }
}