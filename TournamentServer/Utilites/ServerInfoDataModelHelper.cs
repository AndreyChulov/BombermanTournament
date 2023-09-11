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

    private static Action CreateUpdateServerLogsFilePropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerLogFileProperty, 
                (string)server.ServerLogFile));
    }

    private static Action CreateUpdateIsStartStopServerButtonEnabledPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsStartStopServerButtonEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerInfoLabelsEnabledPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerInfoLabelsEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerStartedTextPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerStartedTextProperty, 
                ServerInfoDataModel.GetServerStartedText(server.IsServerStarted)));
    }

    private static Action CreateUpdateStartStopServerButtonTextPropertyAction(
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
            CreateUpdateIsStartStopServerButtonEnabledPropertyAction(dispatcher, dataModel, server));
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsServerInfoLabelsEnabledPropertyAction(dispatcher, dataModel, server));
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateIsServerStartedTextPropertyAction(dispatcher, dataModel, server));        
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateStartStopServerButtonTextPropertyAction(dispatcher, dataModel, server));        
        server.ServerLogFile.OnChanged.AddAction(
            CreateUpdateServerLogsFilePropertyAction(dispatcher, dataModel, server));    
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        CreateUpdateServerPortAction(dispatcher, dataModel, server)();
        CreateUpdateServerAddressAction(dispatcher, dataModel, server)();
        CreateUpdateConnectedClientsCountAction(dispatcher, dataModel, server)();
        CreateUpdateIsStartStopServerButtonEnabledPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateIsServerInfoLabelsEnabledPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateIsServerStartedTextPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateStartStopServerButtonTextPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateServerLogsFilePropertyAction(dispatcher, dataModel, server)();
    }

}