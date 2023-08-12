namespace TournamentServer.Server;

public class ServerStub : IServer
{
    public bool IsServerStarted { get; private set; } = false;
    public void StartServer()
    {
        IsServerStarted = true;
    }

    public void StopServer()
    {
        IsServerStarted = false;
    }
}