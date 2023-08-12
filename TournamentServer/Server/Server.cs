namespace TournamentServer.Server;

public class Server :IServer
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