using TournamentServer.Server;

namespace TournamentServer;

public interface ITournamentServerFormContext
{
    IServer TournamentServer { get; }
}