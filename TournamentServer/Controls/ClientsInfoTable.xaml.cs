using System.Data;
using System.Windows;
using System.Windows.Controls;
using TournamentServer.Resources;
using TournamentServer.Server;
using TournamentServer.Utilites;

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

    private void ClientsInfoTable_OnLoaded(object sender, RoutedEventArgs e)
    {
        var dataModel = Resources["DataModel"] as ClientsInfoTableDataModel;

        if (dataModel == null)
        {
            throw new InvalidConstraintException($"{nameof(ClientsInfoTableDataModel)} should be initialized at this stage");
        }

        ClientsInfoTableDataModelHelper.AddUpdateActions(Dispatcher, dataModel, Server);
        
        dataModel.Server = Server;//Force binding update
    }
}