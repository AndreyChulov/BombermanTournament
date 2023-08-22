using System;
using System.Windows;
using System.Windows.Controls;
using TournamentServer.Server;
using TournamentServer.Utilites;

namespace TournamentServer.Resources;

public partial class ServerInfoDataModel : UserControl
{
    public static readonly DependencyProperty ServerProperty = 
        DependencyProperty.Register(nameof(Server), typeof(IServer), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(IServer), 
                ServerProperty_Changed));

    public static readonly DependencyProperty IsServerStartedTextProperty = 
        DependencyProperty.Register(nameof(IsServerStartedText), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty StartStopServerButtonTextProperty = 
        DependencyProperty.Register(nameof(StartStopServerButtonText), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty IsServerInfoLabelsEnabledProperty = 
        DependencyProperty.Register(nameof(IsServerInfoLabelsEnabled), typeof(bool), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsStartStopServerButtonEnabledProperty = 
        DependencyProperty.Register(nameof(IsStartStopServerButtonEnabled), typeof(bool), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(bool)));
    
    public static readonly DependencyProperty ServerPortProperty = 
        DependencyProperty.Register(nameof(ServerPort), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ServerAddressProperty = 
        DependencyProperty.Register(nameof(ServerAddress), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));
    
    public static readonly DependencyProperty ConnectedClientCountProperty = 
        DependencyProperty.Register(nameof(ConnectedClientCount), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ServerLogFileProperty = 
        DependencyProperty.Register(nameof(ServerLogFile), typeof(string), 
            typeof(ServerInfoDataModel), new PropertyMetadata(default(string)));

    public static void ServerProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IServer server = (IServer)e.NewValue;
        
        ServerInfoDataModelHelper.InvokeUpdateActions(d.Dispatcher, d, server);
    }

    public static string GetStartStopServerButtonText(bool isServerStarted) 
        => isServerStarted ? "Stop server" : "Start server";

    public static string GetServerStartedText(bool isServerStarted) 
        => isServerStarted ? "Server started" : "Server stopped";
    
    public ServerInfoDataModel()
    {
        InitializeComponent();
        
        ServerProperty_Changed(
            this, 
            new DependencyPropertyChangedEventArgs(ServerProperty, null, new ServerStub()));
        //IsServerStartedText = GetServerStartedText(false);
        //StartStopServerButtonText = GetStartStopServerButtonText(false);
        
    }

    public IServer Server
    {
        get => (IServer)GetValue(ServerProperty);
        set => SetValue(ServerProperty, value);
    }
    
    public string IsServerStartedText
    {
        get => (string)GetValue(IsServerStartedTextProperty);
        init => SetValue(IsServerStartedTextProperty, value);
    }

    public string StartStopServerButtonText
    {
        get => (string)GetValue(StartStopServerButtonTextProperty);
        init => SetValue(StartStopServerButtonTextProperty, value);
    }

    public bool IsServerInfoLabelsEnabled
    {
        get => (bool)GetValue(IsServerInfoLabelsEnabledProperty);
        set => SetValue(IsServerInfoLabelsEnabledProperty, value);
    }

    public bool IsStartStopServerButtonEnabled
    {
        get => (bool)GetValue(IsStartStopServerButtonEnabledProperty);
        set => SetValue(IsStartStopServerButtonEnabledProperty, value);
    }

    public string ServerPort
    {
        get => (string)GetValue(ServerPortProperty);
        set => SetValue(ServerPortProperty, value);
    }

    public string ServerAddress
    {
        get => (string)GetValue(ServerAddressProperty);
        set => SetValue(ServerAddressProperty, value);
    }

    public string ConnectedClientCount
    {
        get => (string)GetValue(ConnectedClientCountProperty);
        set => SetValue(ConnectedClientCountProperty, value);
    }

    public string ServerLogFile
    {
        get => (string)GetValue(ServerLogFileProperty);
        set => SetValue(ServerLogFileProperty, value);
    }
}