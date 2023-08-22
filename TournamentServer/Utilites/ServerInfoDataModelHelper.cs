using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ServerInfoDataModelHelper
{
    private static Action CreateUpdateServerPortAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerPortProperty, (string)server.ServerPort));
    }

    private static Action CreateUpdateServerAddressAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerAddressProperty, (string)server.ServerAddress));
    }

    private static Action CreateUpdateConnectedClientsCountAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ConnectedClientCountProperty, 
                (string)server.ClientsConnectedCount));
    }

    private static Action CreateUpdateServerLogsFileAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerLogFileProperty, 
                (string)server.ServerLogFile));
    }

    private static Action CreateUpdateIsStartStopServerButtonEnabledAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsStartStopServerButtonEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerInfoLabelsEnabledAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerInfoLabelsEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerStartedTextAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerStartedTextProperty, 
                ServerInfoDataModel.GetServerStartedText(server.IsServerStarted)));
    }

    private static Action CreateUpdateStartStopServerButtonTextAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.StartStopServerButtonTextProperty, 
                ServerInfoDataModel.GetStartStopServerButtonText(server.IsServerStarted)));
    }

    public static void AddUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        server.ServerPort.OnChanged.AddAction(
            CreateUpdateServerPortAction(dispatcher, dataModel, server));
        server.ServerAddress.OnChanged.AddAction(
            CreateUpdateServerAddressAction(dispatcher, dataModel, server));
        server.ClientsConnectedCount.OnChanged.AddAction(
            CreateUpdateConnectedClientsCountAction(dispatcher, dataModel, server));
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsStartStopServerButtonEnabledAction(dispatcher, dataModel, server));
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsServerInfoLabelsEnabledAction(dispatcher, dataModel, server));
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateIsServerStartedTextAction(dispatcher, dataModel, server));        
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateStartStopServerButtonTextAction(dispatcher, dataModel, server));        
        server.ServerLogFile.OnChanged.AddAction(
            CreateUpdateServerLogsFileAction(dispatcher, dataModel, server));    
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        CreateUpdateServerPortAction(dispatcher, dataModel, server)();
        CreateUpdateServerAddressAction(dispatcher, dataModel, server)();
        CreateUpdateConnectedClientsCountAction(dispatcher, dataModel, server)();
        CreateUpdateIsStartStopServerButtonEnabledAction(dispatcher, dataModel, server)();
        CreateUpdateIsServerInfoLabelsEnabledAction(dispatcher, dataModel, server)();
        CreateUpdateIsServerStartedTextAction(dispatcher, dataModel, server)();
        CreateUpdateStartStopServerButtonTextAction(dispatcher, dataModel, server)();
        CreateUpdateServerLogsFileAction(dispatcher, dataModel, server)();
    }

}