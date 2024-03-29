using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class TournamentServerFormControlDataModelHelper
{
    private static Action CreateUpdateIsButtonStartTournamentEnabledPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(TournamentServerFormControlDataModel.IsButtonStartTournamentEnabledProperty, 
                server.IsServerStarted && server.IsClientsReadyForTournament && !server.IsServerProcessingCommand));
    }

    public static void AddUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));        
        server.IsClientsReadyForTournament.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));        
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server));        
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        CreateUpdateIsButtonStartTournamentEnabledPropertyAction(dispatcher, dataModel, server)();
    }

}