namespace TournamentServer.Server;

public interface IServer
{
    bool IsServerStarted { get; }

    void StartServer();
    void StopServer();
}