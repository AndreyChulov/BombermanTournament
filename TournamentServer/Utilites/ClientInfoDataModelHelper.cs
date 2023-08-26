using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ClientInfoDataModelHelper
{
    private static Action CreateUpdateIsButtonStartTournamentEnabledPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(TournamentServerFormControlDataModel.IsButtonStartTournamentEnabledProperty, 
                server.IsServerStarted && server.IsClientConnected && !server.IsServerProcessingCommand));
    }

    public static void AddUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IConnectedClientInfo server)
    {
        /*server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));        
        server.IsClientConnected.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));        
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));*/        
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IConnectedClientInfo server)
    {
        //CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server)();
    }

}