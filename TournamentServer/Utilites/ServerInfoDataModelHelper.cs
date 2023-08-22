using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ServerInfoDataModelHelper
{
    private static Action CreateUpdateServerPortForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerPortProperty, (string)server.ServerPort));
    }

    private static Action CreateUpdateServerAddressForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerAddressProperty, (string)server.ServerAddress));
    }

    private static Action CreateUpdateConnectedClientsCountForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ConnectedClientCountProperty, 
                (string)server.ClientsConnected));
    }

    private static Action CreateUpdateServerLogsFileForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.ServerLogFileProperty, 
                (string)server.ServerLogFile));
    }

    private static Action CreateUpdateIsStartStopServerButtonEnabledForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsStartStopServerButtonEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerInfoLabelsEnabledForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerInfoLabelsEnabledProperty, 
                !(bool)server.IsServerProcessingCommand));
    }

    private static Action CreateUpdateIsServerStartedTextForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ServerInfoDataModel.IsServerStartedTextProperty, 
                ServerInfoDataModel.GetServerStartedText(server.IsServerStarted)));
    }

    private static Action CreateUpdateStartStopServerButtonTextForServerInfoControl(
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
            CreateUpdateServerPortForServerInfoControl(dispatcher, dataModel, server));
        server.ServerAddress.OnChanged.AddAction(
            CreateUpdateServerAddressForServerInfoControl(dispatcher, dataModel, server));
        server.ClientsConnected.OnChanged.AddAction(
            CreateUpdateConnectedClientsCountForServerInfoControl(dispatcher, dataModel, server));
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsStartStopServerButtonEnabledForServerInfoControl(dispatcher, dataModel, server));
        server.IsServerProcessingCommand.OnChanged.AddAction(
            CreateUpdateIsServerInfoLabelsEnabledForServerInfoControl(dispatcher, dataModel, server));
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateIsServerStartedTextForServerInfoControl(dispatcher, dataModel, server));        
        server.IsServerStarted.OnChanged.AddAction(
            CreateUpdateStartStopServerButtonTextForServerInfoControl(dispatcher, dataModel, server));        
        server.ServerLogFile.OnChanged.AddAction(
            CreateUpdateServerLogsFileForServerInfoControl(dispatcher, dataModel, server));    
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        CreateUpdateServerPortForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateServerAddressForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateConnectedClientsCountForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateIsStartStopServerButtonEnabledForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateIsServerInfoLabelsEnabledForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateIsServerStartedTextForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateStartStopServerButtonTextForServerInfoControl(dispatcher, dataModel, server)();
        CreateUpdateServerLogsFileForServerInfoControl(dispatcher, dataModel, server)();
    }

}