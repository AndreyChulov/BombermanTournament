using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ControlDataModelHelper
{
    private static Action CreateUpdateAction(
        Dispatcher dispatcher, 
        PropertyChangedCallback propertyChangedCallback,
        DependencyObject dataModel, 
        DependencyProperty serverProperty,
        IServer server)
    {
        return 
            () => dispatcher.Invoke(
                propertyChangedCallback,
                dataModel, 
                new DependencyPropertyChangedEventArgs(
                    serverProperty, 
                    null, 
                    server));
    }

    public static Action CreateUpdateDataModelActionForTournamentFormControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return ControlDataModelHelper.CreateUpdateAction(
            dispatcher,
            TournamentServerFormControlDataModel.ServerProperty_Changed,
            dataModel,
            TournamentServerFormControlDataModel.ServerProperty,
            server);
    }
    
    public static Action CreateUpdateDataModelActionForServerInfoControl(
        Dispatcher dispatcher, DependencyObject dataModel, IServer server)
    {
        return ControlDataModelHelper.CreateUpdateAction(
            dispatcher,
            ServerInfoDataModel.ServerProperty_Changed,
            dataModel,
            ServerInfoDataModel.ServerProperty,
            server);
    }
}