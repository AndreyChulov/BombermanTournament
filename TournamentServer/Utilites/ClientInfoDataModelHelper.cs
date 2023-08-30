using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ClientInfoDataModelHelper
{
    private static Action CreateUpdateClientIPPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ClientInfoDataModel.ClientIPProperty, 
                ((ConnectedClientInfoArray)server.ClientsConnectedInfoArray)[dataModel.ClientIndex]
                    .ConnectedClientId.ClientIP));
    }

    private static Action CreateUpdateClientPortPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ClientInfoDataModel.ClientPortProperty, 
                ((ConnectedClientInfoArray)server.ClientsConnectedInfoArray)[dataModel.ClientIndex]
                    .ConnectedClientId.ClientPort));
    }

    public static void AddUpdateActions(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        
        ((ConnectedClientInfoArray)server.ClientsConnectedInfoArray)[dataModel.ClientIndex]
            .SetOnClientUpdatedAction(() => InvokeUpdateActions(dispatcher, dataModel, server));
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        CreateUpdateClientIPPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateClientPortPropertyAction(dispatcher, dataModel, server)();
    }

}