using System.Data;
using System.Windows;
using System.Windows.Controls;
using TournamentServer.Resources;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Controls;

public partial class ClientInfo : UserControl
{
    public static readonly DependencyProperty TitleProperty = 
        DependencyProperty.Register(nameof(Title), typeof(string), 
            typeof(ClientInfo), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ClientIndexProperty = 
        DependencyProperty.Register(nameof(ClientIndex), typeof(int), 
            typeof(ClientInfo), new PropertyMetadata(default(int)));

    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ClientInfo), new PropertyMetadata(default(IServer)));

    public ClientInfo()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public int ClientIndex
    {
        get => (int)GetValue(ClientIndexProperty);
        set => SetValue(ClientIndexProperty, value);
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    private void ClientInfo_OnLoaded(object sender, RoutedEventArgs e)
    {
        var dataModel = Resources["DataModel"] as ClientInfoDataModel;

        if (dataModel == null)
        {
            throw new InvalidConstraintException($"{nameof(ClientInfoDataModel)} should be initialized at this stage");
        }

        ClientInfoDataModelHelper.AddUpdateActions(Dispatcher, dataModel, Server);
        
        dataModel.ClientIndex = ClientIndex;
        dataModel.Server = Server;//Force binding update
        dataModel.Title = Title;
    }
}