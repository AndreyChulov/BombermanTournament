using TournamentServer.Shared;

namespace Games.BombermanGame;

public class BombermanNetworkGame
{
    private BombermanNetworkBot[] _bots;
    
    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        _bots = clients.Select(x => new BombermanNetworkBot(x)).ToArray();
        throw new NotImplementedException();
    }
    
    
}