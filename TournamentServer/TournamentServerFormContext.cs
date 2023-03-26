using TournamentServer.Server;

namespace TournamentServer;

public class TournamentServerFormContext : ITournamentServerFormContext
{
    public IServer TournamentServer { get; }
    
    public TournamentServerFormContext()
    {
        TournamentServer = new Server.Server();
    }

}