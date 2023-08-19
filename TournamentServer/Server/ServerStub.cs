using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class ServerStub : IServer
{
    public MonitoredVariable<bool> IsServerStarted => false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientConnected { get; } = false;

    public void StartServer()
    {
        IsServerStarted.SetVariable(true);
    }

    public void StopServer()
    {
        IsServerStarted.SetVariable(false);
    }
}