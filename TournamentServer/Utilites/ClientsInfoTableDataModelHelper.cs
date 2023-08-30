using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ClientsInfoTableDataModelHelper
{
    private static Action CreateUpdateClientsInfoPropertyAction(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return () => dispatcher.Invoke(
            () => dataModel.SetValue(ClientsInfoTableDataModel.ServerProperty, server));
    }

    public static void AddUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        server.ClientsConnectedInfoArray.OnChanged.AddAction(
            CreateUpdateClientsInfoPropertyAction(dispatcher, dataModel, server));
    }

    public static void InvokeUpdateActions(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        CreateUpdateClientsInfoPropertyAction(dispatcher, dataModel, server)();
    }

}