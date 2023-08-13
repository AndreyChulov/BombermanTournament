using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using TournamentServer.Resources;
using TournamentServer.Server;

namespace TournamentServer.Controls;

public partial class ServerInfo : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), typeof(ServerInfo), 
            new PropertyMetadata(default(IServer)));

    public ServerInfo()
    {
        InitializeComponent();
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }

    private void StartStopServerButton_OnClick(object sender, RoutedEventArgs e)
    {
        
        if (Server.IsServerStarted)
        {
            Server.StopServer();
        }
        else
        {
            Server.StartServer();
        }
        
    }

    private void ServerInfo_OnLoaded(object sender, RoutedEventArgs e)
    {
        var dataModel = Resources["DataModel"] as DependencyObject;

        if (dataModel == null)
        {
            throw new InvalidConstraintException($"{nameof(ServerInfoDataModel)} should be initialized at this stage");
        }
        
        Server.IsServerStarted.OnChanged = 
            (Action)(() => ServerInfoDataModel.ServerProperty_Changed(dataModel, 
                new DependencyPropertyChangedEventArgs(ServerProperty, null, Server)));
    }
}