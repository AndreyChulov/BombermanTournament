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

    private ReadOnlyDictionary<PlayerTurnEnum, Func<PlayerInfo, int, FieldItemEnum?>> _getFieldNextCellDictionary;
    private ReadOnlyDictionary<PlayerTurnEnum, Action<PlayerInfo>> _movePlayerDictionary;

    private readonly Timer _turnTimer;
    private List<Bomb> _bombs = new List<Bomb>();

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
            TimeSpan.Zero, 
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

    private ReadOnlyDictionary<PlayerTurnEnum, Func<PlayerInfo, int, FieldItemEnum?>> PopulateGetFieldNextCellDictionary()
    {
        return new(new Dictionary<PlayerTurnEnum, Func<PlayerInfo, int, FieldItemEnum?>>
        {
            { PlayerTurnEnum.None, (_, _) => null },
            { PlayerTurnEnum.MoveRight, Field.GetRightFieldItem },
            { PlayerTurnEnum.MoveLeft, Field.GetLeftFieldItem },
            { PlayerTurnEnum.MoveDown, Field.GetDownFieldItem },
            { PlayerTurnEnum.PutBomb, (_, _) => null },
            { PlayerTurnEnum.MoveUp, Field.GetUpFieldItem },
        });
    }

    private void OnTurn(Dictionary<int, PlayerTurnEnum> botCommands)
    {
        foreach (var (index, command) in botCommands)
        {
            ApplyPlayerCommand(index, command);
        }
        
        ExplodeBombs();
        
        foreach (var bomb in _bombs)
        {
            bomb.Tick();
        }
        
        Field.ForceFieldUpdated();
    }

    private void ApplyPlayerCommand(int index, PlayerTurnEnum command)
    {
        var playerInfo =
            (PlayerInfo)PlayerCollectionMediator.PlayersInfo.GetPlayerInfo(index);
        
        ApplyMovingBotCommand(playerInfo, index, command);
        ApplyPutBombBotCommand(command, playerInfo);
    }

    private void ExplodeBombs()
    {
        var bombsToExplode = _bombs.Where(x => x.TicksToBoom == 0);

        foreach (var bomb in bombsToExplode)
        {
            var currentFieldItem = Field.GetCurrentFieldItem(bomb);
            var fieldWithoutBomb = currentFieldItem.RemoveBombFromFieldItem();

            Field.SetFieldCell(bomb, fieldWithoutBomb);

            var breakFields = new[] { FieldItemEnum.IndestructibleField };
            var crossFields = Field.GetCrossFields(
                bomb, 1, breakFields);

            foreach (var crossField in crossFields)
            {
                if (crossField.IsPlayerOnField())
                {
                    PlayerCollectionMediator.GetPlayer(crossField).BlowUpPlayer();
                }
                
                if (crossField == FieldItemEnum.DestructibleField)
                {
                    bomb.Owner.PlayerBlowUpDestroyableCell();
                }
            }
        }

        _bombs = _bombs.Where(x => x.TicksToBoom != 0).ToList();
    }

    private void ApplyPutBombBotCommand(PlayerTurnEnum command, PlayerInfo playerInfo)
    {
        if (command != PlayerTurnEnum.PutBomb)
        {
            return;
        }
        
        var currentFieldItem = Field.GetCurrentFieldItem(playerInfo);

        if (currentFieldItem.IsBombOnField())
        {
            return;
        }
        
        var fieldItemWithBomb = currentFieldItem.AddBombToFieldItem();
                
        Field.SetFieldCell(playerInfo, fieldItemWithBomb);
                
        _bombs.Add(new Bomb(playerInfo));
    }

    private void ApplyMovingBotCommand(
        PlayerInfo playerInfo, int index, PlayerTurnEnum command)
    {
        if (_getFieldNextCellDictionary[command](playerInfo, 1) != FieldItemEnum.EmptyField)
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