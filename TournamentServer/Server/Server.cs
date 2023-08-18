using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class Server :IServer
{
    //private I
    
    public MonitoredVariable<bool> IsServerStarted { get; } = false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;

    public void StartServer()
    {
        IsServerStarted.SetVariable(true);
        IsServerProcessingCommand.SetVariable(true);
    }

    public void StopServer()
    {
        IsServerStarted.SetVariable(false);
        IsServerProcessingCommand.SetVariable(true);
    }
}