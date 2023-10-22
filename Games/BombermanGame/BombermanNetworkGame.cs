using Games.BombermanGame.Shared.GameDataModel;
using TournamentServer.Shared;

namespace Games.BombermanGame;

public class BombermanNetworkGame
{
    private BombermanNetworkBot[] _bots;
    private Field _field;
    
    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        _bots = clients.Select(x => new BombermanNetworkBot(x)).ToArray();
        _field = InitializeField();
        
        throw new NotImplementedException();
    }
    
    private Field InitializeField()
    {
        var levelGenerator = LevelGenerator.Initialize();
        Field field = levelGenerator.CreateField();
        
        levelGenerator.FillFieldStartCells(field);
        levelGenerator.FillPlayersStartPoints(field);
        
        return field;
    }
}