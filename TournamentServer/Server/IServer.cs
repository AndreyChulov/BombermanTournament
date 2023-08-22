using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IServer
{
    MonitoredVariable<bool> IsServerStarted { get; }
    MonitoredVariable<bool> IsServerProcessingCommand { get; }
    MonitoredVariable<bool> IsClientConnected { get; }
    MonitoredVariable<string> ServerAddress { get; }
    MonitoredVariable<string> ServerPort { get; }
    MonitoredVariable<string> ClientsConnected { get; }
    MonitoredVariable<string> ServerLogFile { get; }
    
    void StartServer();
    void StopServer();
    
    
}