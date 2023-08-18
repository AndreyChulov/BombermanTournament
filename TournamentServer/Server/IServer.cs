using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IServer
{
    MonitoredVariable<bool> IsServerStarted { get; }
    MonitoredVariable<bool> IsServerProcessingCommand { get; }
    
    void StartServer();
    void StopServer();
    
    
}