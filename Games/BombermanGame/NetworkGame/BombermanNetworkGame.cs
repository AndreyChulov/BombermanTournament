using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkGame
{
    //private BombermanNetworkBot[] _bots;
    private BombermanNetworkGameForm _form;

    public Field Field { get; }
    public PlayerCollection Players { get; }
    public PlayerInfoCollection PlayersInfo { get; }

    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        Players = new PlayerCollection(
            clients
                .Select(x => new BombermanNetworkBot(x))
                .Cast<IPlayer>()
                .ToArray());
        Field = InitializeField();
        PlayersInfo = InitializePlayersInfo();
        _form = new BombermanNetworkGameForm(this);

        Task.Run(() => Application.Run(_form)); 
        
        //throw new NotImplementedException();
    }

    private PlayerInfoCollection InitializePlayersInfo()
    {
        PlayerInfo? player1Info = null;
        PlayerInfo? player2Info = null;
        PlayerInfo? player3Info = null;
        PlayerInfo? player4Info = null;
        
        Field.EnumerateField<int>((rowIndex, columnIndex, cell) =>
        {
            switch (cell)
            {
                case FieldItemEnum.Player1:
                    player1Info = new PlayerInfo(Players.Player1, columnIndex, rowIndex);
                    return null;
                case FieldItemEnum.Player2:
                    player2Info = new PlayerInfo(Players.Player2, columnIndex, rowIndex);
                    return null;
                case FieldItemEnum.Player3:
                    player3Info = new PlayerInfo(Players.Player3, columnIndex, rowIndex);
                    return null;                
                case FieldItemEnum.Player4:
                    player4Info = new PlayerInfo(Players.Player4, columnIndex, rowIndex);
                    return null;        
                default:
                    return null;
            }
        });

        if (player1Info == null || player2Info == null || player3Info == null || player4Info == null)
        {
            throw new InvalidOperationException($"Unexpected state: not 4 players on field");
        }

        return new PlayerInfoCollection(player1Info, player2Info, player3Info, player4Info);
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