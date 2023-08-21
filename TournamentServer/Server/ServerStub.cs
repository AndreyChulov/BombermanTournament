using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class ServerStub : IServer
{
    public MonitoredVariable<bool> IsServerStarted => false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientConnected { get; } = false;
    public MonitoredVariable<string> ServerAddress { get; } = "0.0.0.0";
    public MonitoredVariable<string> ServerPort { get; } = "-888";
    public MonitoredVariable<string> ClientsConnected { get; } = "0";

    public void StartServer()
    {
        IsServerStarted.SetVariable(true);
    }

    public void StopServer()
    {
        IsServerStarted.SetVariable(false);
    }
}