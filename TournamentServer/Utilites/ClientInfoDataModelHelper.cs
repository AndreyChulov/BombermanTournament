using System;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;
using TournamentServer.Shared;

namespace TournamentServer.Utilites;

public static class ClientInfoDataModelHelper
{
    private static Action CreateUpdateNicknamePropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.NickNameProperty, (string)clientInfo.NickName);
            });
    }

    private static Action CreateUpdateReadyForTournamentTextPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.ReadyForTournamentTextProperty, 
                    clientInfo.IsReadyForTournamentStart ? "Yes" : "No");
            });
    }
    
    private static Action CreateUpdateIsInDebugModePropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.IsOnDebugModeProperty, 
                    (bool)clientInfo.IsDebugMode);
            });
    }

    private static Action CreateUpdateStrategyDescriptionPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.StrategyDescriptionProperty, 
                    (string)clientInfo.StrategyDescription);
            });
    }
    
    private static Action CreateUpdateGamePropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.GameProperty, 
                    (string)clientInfo.Game);
            });
    }

    private static Action CreateUpdateClientIPPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.ClientIPProperty, clientInfo.ConnectedClientId.ClientIP);
            });
    }

    private static Action CreateUpdateClientPortPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                dataModel.SetValue(ClientInfoDataModel.ClientPortProperty, clientInfo.ConnectedClientId.ClientPort);
            });
    }

    private static Action CreateUpdateClientsConnectedInfoArrayPropertyAction(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () =>
            {
                var clientsConnectedInfoArray = (ConnectedClientInfoArray)server.ClientsConnectedInfoArray;
                var clientInfo = clientsConnectedInfoArray[dataModel.ClientIndex];
                
                clientInfo.SetOnClientUpdatedAction(() => InvokeUpdateActions(dispatcher, dataModel, server));
                clientInfo.NickName.OnChanged.AddAction(CreateUpdateNicknamePropertyAction(dispatcher, dataModel, server));
                clientInfo.StrategyDescription.OnChanged.AddAction(
                    CreateUpdateStrategyDescriptionPropertyAction(dispatcher, dataModel, server));
                clientInfo.IsReadyForTournamentStart.OnChanged.AddAction(
                    CreateUpdateReadyForTournamentTextPropertyAction(dispatcher, dataModel, server));
                clientInfo.Game.OnChanged.AddAction(
                    CreateUpdateGamePropertyAction(dispatcher, dataModel, server));
                clientInfo.IsDebugMode.OnChanged.AddAction(
                    CreateUpdateIsInDebugModePropertyAction(dispatcher, dataModel, server));
                
                InvokeUpdateActions(dispatcher, dataModel, server);
            });
    }
    
    public static void AddUpdateActions(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        server.ClientsConnectedInfoArray.OnChanged.AddAction(
            CreateUpdateClientsConnectedInfoArrayPropertyAction(dispatcher, dataModel, server));
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, ClientInfoDataModel dataModel, IServer server)
    {
        CreateUpdateClientIPPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateClientPortPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateNicknamePropertyAction(dispatcher, dataModel, server)();
        CreateUpdateStrategyDescriptionPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateReadyForTournamentTextPropertyAction(dispatcher, dataModel, server)();
        CreateUpdateGamePropertyAction(dispatcher, dataModel, server)();
        CreateUpdateIsInDebugModePropertyAction(dispatcher, dataModel, server)();
    }

}