using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IServer
{
    MonitoredVariable<bool> IsServerStarted { get; }
    MonitoredVariable<bool> IsServerProcessingCommand { get; }
    MonitoredVariable<bool> IsClientConnected { get; }
    
    void StartServer();
    void StopServer();
    
    
}