using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkGame
{
    private BombermanNetworkGameForm _form;

    public Field Field { get; }
    public PlayerCollectionMediator PlayerCollectionMediator { get; }


    public BombermanNetworkGame(IConnectedClientInfo[] clients)
    {
        PlayerCollectionMediator = new PlayerCollectionMediator();
        PlayerCollectionMediator.SetPlayers(clients);
        PlayerCollectionMediator.SetTurnAction(OnTurn);
        
        Field = InitializeField();
        
        PlayerCollectionMediator.InitializePlayersInfo(Field);

        _form = new BombermanNetworkGameForm(this);

        Task.Run(() => Application.Run(_form));

        PlayerCollectionMediator.Turn(Field);
        //Parallel.ForEach(Players.Players, (player, state) => {state.});
    }

    private void OnTurn(Dictionary<int, PlayerTurnEnum> botCommands)
    {
        Field.EnumerateField<int>((row, column, cell) =>
        {
            if (cell.IsPlayerOnField())
            {
                foreach (var (index, command) in botCommands)
                {
                    if (cell.IsPlayerOnField(index))
                    {
                        switch (command)
                        {
                            case PlayerTurnEnum.MoveDown:
                                return ApplyPlayerCommand(index, cell);
                            case PlayerTurnEnum.MoveRight:
                            case PlayerTurnEnum.MoveLeft:
                            case PlayerTurnEnum.MoveUp:
                            case PlayerTurnEnum.PutBomb:
                            case PlayerTurnEnum.None:
                                break;
                        }
                    }
                }
            }
            if (cell.IsPlayerOnField(Index))
            switch (cell)
            {
                
            }
        });
    }

    private int? ApplyPlayerCommand(int index, FieldItemEnum cell)
    {
        var playerInfo =
            (PlayerInfo)PlayerCollectionMediator.PlayersInfo.GetPlayerInfo(index);
        if (Field.GetDownFieldItem(playerInfo) != FieldItemEnum.EmptyField)
        {
            return null;
        }

        Field.SetFieldCell(playerInfo, cell.RemovePlayerFromFieldItem());
        playerInfo.MoveDown();
        Field.SetFieldCell(playerInfo, FieldItemEnum.EmptyField.GetPlayerFieldItem(index));

        return null;
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
}