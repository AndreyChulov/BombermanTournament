using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IServer
{
    MonitoredVariable<bool> IsServerStarted { get; }
    
    void StartServer();
    void StopServer();
    
    
}