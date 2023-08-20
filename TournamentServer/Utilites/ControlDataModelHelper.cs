using System;
using System.Windows;
using System.Windows.Threading;
using TournamentServer.Server;

namespace TournamentServer.Utilites;

public static class ControlDataModelHelper
{
    public static Action CreateUpdateAction(
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
}