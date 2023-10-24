using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.Interfaces;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkGame
{
    //private BombermanNetworkBot[] _bots;
    private PlayerCollection _players;
    private BombermanNetworkGameForm _form;

    public Field Field { get; }

    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        _players = new PlayerCollection(
            clients
                .Select(x => new BombermanNetworkBot(x))
                .Cast<IPlayer>()
                .ToArray());
        Field = InitializeField();
        _form = new BombermanNetworkGameForm(this);

        Task.Run(() => Application.Run(_form)); 
        
        //throw new NotImplementedException();
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