using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
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

        SetPlayersOnField(field);
        
        field.SetFieldCell(5,5, FieldItemEnum.Bomb);
        
        return field;
    }

    private static void SetPlayersOnField(Field field)
    {
        field.EnumerateField((row, column, cell) =>
        {
            if (!cell.IsPlayerStartPointOnField())
            {
                return null;
            }

            field.SetFieldCell(row, column, cell.GetPlayerFromStartPoint());

            return (int?)null;
        });
    }
}