using System.Collections.Generic;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IServer
{
    MonitoredVariable<bool> IsServerStarted { get; }
    MonitoredVariable<bool> IsServerProcessingCommand { get; }
    MonitoredVariable<bool> IsClientsReadyForTournament { get; }
    MonitoredVariable<string> ServerAddress { get; }
    MonitoredVariable<string> ServerPort { get; }
    MonitoredVariable<string> ClientsConnectedCount { get; }
    MonitoredVariable<ConnectedClientInfoArray> ClientsConnectedInfoArray { get; }
    MonitoredVariable<string> ServerLogFile { get; }
    
    void StartServer();
    void StopServer();
    void StartTournament();

}