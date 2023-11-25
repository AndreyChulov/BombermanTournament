using System.Collections.ObjectModel;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using TournamentServer.Shared;
using Timer = System.Threading.Timer;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkGame : IDisposable
{
    private BombermanNetworkGameForm _form;

    public Field Field { get; }
    public PlayerCollectionMediator PlayerCollectionMediator { get; }

    private ReadOnlyDictionary<PlayerTurnEnum, Func<PlayerInfo, FieldItemEnum?>> _getFieldNextCellDictionary;
    private ReadOnlyDictionary<PlayerTurnEnum, Action<PlayerInfo>> _movePlayerDictionary;

    private Timer _turnTimer;


    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        PlayerCollectionMediator = new PlayerCollectionMediator();
        PlayerCollectionMediator.SetPlayers(clients);
        PlayerCollectionMediator.SetTurnAction(OnTurn);
        
        Field = InitializeField();

        _getFieldNextCellDictionary = PopulateGetFieldNextCellDictionary();
        _movePlayerDictionary = PopulateMovePlayerDictionary();
        
        PlayerCollectionMediator.InitializePlayersInfo(Field);

        _form = new BombermanNetworkGameForm(this);

        Task.Run(() => Application.Run(_form));

        _turnTimer = new Timer(TurnTimer_Callback);
        _turnTimer.Change(
            NetworkGameSettings.FirstTurnTimeoutForServer, 
            Timeout.InfiniteTimeSpan
            );
    }

    private void TurnTimer_Callback(object? state)
    {
        PlayerCollectionMediator.Turn(Field);
        
        _turnTimer.Change(
            NetworkGameSettings.TurnTimeoutForServer, 
            Timeout.InfiniteTimeSpan
            );
    }

    private static ReadOnlyDictionary<PlayerTurnEnum, Action<PlayerInfo>> PopulateMovePlayerDictionary()
    {
        return new(new Dictionary<PlayerTurnEnum, Action<PlayerInfo>>
        {
            { PlayerTurnEnum.None, _ => { } },
            { PlayerTurnEnum.MoveDown, playerInfo => playerInfo.MoveDown() },
            { PlayerTurnEnum.MoveRight, playerInfo => playerInfo.MoveRight() },
            { PlayerTurnEnum.MoveLeft, playerInfo => playerInfo.MoveLeft() },
            { PlayerTurnEnum.PutBomb, _ => { } },
            { PlayerTurnEnum.MoveUp, playerInfo => playerInfo.MoveUp() },
        });
    }

    private ReadOnlyDictionary<PlayerTurnEnum, Func<PlayerInfo, FieldItemEnum?>> PopulateGetFieldNextCellDictionary()
    {
        return new(new Dictionary<PlayerTurnEnum, Func<PlayerInfo, FieldItemEnum?>>
        {
            { PlayerTurnEnum.None, _ => null },
            { PlayerTurnEnum.MoveRight, Field.GetRightFieldItem },
            { PlayerTurnEnum.MoveLeft, Field.GetLeftFieldItem },
            { PlayerTurnEnum.MoveDown, Field.GetDownFieldItem },
            { PlayerTurnEnum.PutBomb, _ => null },
            { PlayerTurnEnum.MoveUp, Field.GetUpFieldItem },
        });
    }

    private void OnTurn(Dictionary<int, PlayerTurnEnum> botCommands)
    {
        foreach (var (index, command) in botCommands)
        {
            ApplyPlayerCommand(index, command);
        }
        
        Field.ForceFieldUpdated();
    }

    private void ApplyPlayerCommand(int index, PlayerTurnEnum command)
    {
        var playerInfo =
            (PlayerInfo)PlayerCollectionMediator.PlayersInfo.GetPlayerInfo(index);
        
        if (_getFieldNextCellDictionary[command](playerInfo) != FieldItemEnum.EmptyField)
        {
            return;
        }

        Field.SetFieldCell(
            playerInfo, 
            Field.GetCurrentFieldItem(playerInfo).RemovePlayerFromFieldItem()
            );
        _movePlayerDictionary[command](playerInfo);
        Field.SetFieldCell(
            playerInfo, 
            FieldItemEnum.EmptyField.GetPlayerFieldItem(index, true)
            );
    }


    private Field InitializeField()
    {
        var levelGenerator = LevelGenerator.Initialize();
        Field field = levelGenerator.CreateField();
        
        levelGenerator.FillFieldStartCells(field);
        levelGenerator.FillPlayersStartPoints(field);

        SetPlayersOnField(field);
        
        field.SetFieldCell(5,5, FieldItemEnum.Bomb);
        
        field.ForceFieldUpdated();
        
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

    public void Dispose()
    {
        _form.Close();
        _form.Dispose();
        PlayerCollectionMediator.Dispose();
    }
}