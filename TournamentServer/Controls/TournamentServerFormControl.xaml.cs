using System.Data;
using System.Windows;
using System.Windows.Controls;
using TournamentServer.Resources;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Controls;

public partial class TournamentServerFormControl : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(TournamentServerFormControl), new PropertyMetadata(default(IServer)));

    public TournamentServerFormControl()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    private void BtnStartTournament_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void TournamentServerFormControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        var dataModel = Resources["DataModel"] as TournamentServerFormControlDataModel;
        
        if (dataModel == null)
        {
            throw new InvalidConstraintException($"{nameof(TournamentServerFormControlDataModel)} should be initialized at this stage");
        }
        
        Server.IsServerStarted.OnChanged.AddAction(
            ControlDataModelHelper.CreateUpdateAction(
                Dispatcher,
                TournamentServerFormControlDataModel.ServerProperty_Changed,
                dataModel,
                TournamentServerFormControlDataModel.ServerProperty,
                Server));
        Server.IsClientConnected.OnChanged.AddAction(
            ControlDataModelHelper.CreateUpdateAction(
                Dispatcher,
                TournamentServerFormControlDataModel.ServerProperty_Changed,
                dataModel,
                TournamentServerFormControlDataModel.ServerProperty,
                Server));
        Server.IsServerProcessingCommand.OnChanged.AddAction(
            ControlDataModelHelper.CreateUpdateAction(
                Dispatcher,
                TournamentServerFormControlDataModel.ServerProperty_Changed,
                dataModel,
                TournamentServerFormControlDataModel.ServerProperty,
                Server));

        
        dataModel.Server = Server;//Force binding update
    }

}