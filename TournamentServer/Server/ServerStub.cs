using System;
using TournamentServer.Shared;
using TournamentServer.Shared.Utilities;

namespace TournamentServer.Server;

public class ServerStub : IServer
{
    public MonitoredVariable<bool> IsServerStarted => false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientsReadyForTournament { get; } = false;
    public MonitoredVariable<string> ServerAddress { get; } = "0.0.0.0";
    public MonitoredVariable<string> ServerPort { get; } = "-888";
    public MonitoredVariable<string> ClientsConnectedCount { get; } = "-1";
    public MonitoredVariable<ConnectedClientInfoArray> ClientsConnectedInfoArray { get; }= 
        (ConnectedClientInfoArray)Array.Empty<IConnectedClientInfo>();
    public MonitoredVariable<string> ServerLogFile { get; } = "server.log";

    public void StartServer()
    {
        IsServerStarted.SetVariable(true);
    }

    public void StopServer()
    {
        IsServerStarted.SetVariable(false);
    }

    public void StartTournament()
    {
        
    }
}