using TournamentServer.Server;

namespace TournamentServer;

public class TournamentServerFormDesignContext : ITournamentServerFormContext
{
    public IServer TournamentServer { get; }
    
    public TournamentServerFormDesignContext()
    {
        TournamentServer = new ServerStub();
    }

}