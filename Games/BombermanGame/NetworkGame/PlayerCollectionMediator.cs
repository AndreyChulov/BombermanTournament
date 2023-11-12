using System.Collections.Concurrent;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class PlayerCollectionMediator
{
    private PlayerCollection? _players;
    private PlayerInfoCollection? _playersInfo;
    private ConcurrentDictionary<long, PlayerTurnEnum> _playersTurn = new();
    private Action<Dictionary<int, PlayerTurnEnum>>? _turnAction = null;

    public void SetTurnAction(Action<Dictionary<int, PlayerTurnEnum>> turnAction)
    {
        _turnAction = turnAction;
    }

    public PlayerCollection Players
    {
        get
        {
            if (_players == null)
            {
                throw new InvalidOperationException(
                    $"[{nameof(_players)}] should be initialised before getting [{nameof(Players)}].\n" +
                    $"Please check [{nameof(SetPlayers)}] call.");
            }
    
            return _players;
        }
    }

    public PlayerInfoCollection PlayersInfo
    {
        get
        {
            if (_playersInfo == null)
            {
                throw new InvalidOperationException(
                    $"[{nameof(_playersInfo)}] should be initialised before getting [{nameof(PlayersInfo)}].\n" +
                    $"Please check [{nameof(InitializePlayersInfo)}] call.");
            }

            return _playersInfo;            
        }
    }

    public void SetPlayers(IConnectedClientInfo[] clients)
    {
        _players = new PlayerCollection(
            clients
                .Select(x => new BombermanNetworkBot(x))
                .Cast<IPlayer>()
                .ToArray());
    }
    
    public void InitializePlayersInfo(Field field)
    {
        if (_players == null)
        {
            throw new InvalidOperationException(
                $"[{nameof(_players)}] should be initialised before [{nameof(InitializePlayersInfo)}].\n" +
                $"Please check [{nameof(SetPlayers)}] call.");
        }
        
        PlayerInfo? player1Info = null;
        PlayerInfo? player2Info = null;
        PlayerInfo? player3Info = null;
        PlayerInfo? player4Info = null;
        
        field.EnumerateField<int>((rowIndex, columnIndex, cell) =>
        {
            switch (cell)
            {
                case FieldItemEnum.Player1:
                    player1Info = new PlayerInfo(_players.Player1, columnIndex, rowIndex);
                    return null;
                case FieldItemEnum.Player2:
                    player2Info = new PlayerInfo(_players.Player2, columnIndex, rowIndex);
                    return null;
                case FieldItemEnum.Player3:
                    player3Info = new PlayerInfo(_players.Player3, columnIndex, rowIndex);
                    return null;                
                case FieldItemEnum.Player4:
                    player4Info = new PlayerInfo(_players.Player4, columnIndex, rowIndex);
                    return null;        
                default:
                    return null;
            }
        });

        if (player1Info == null || player2Info == null || player3Info == null || player4Info == null)
        {
            throw new InvalidOperationException($"Unexpected state: not 4 players on field");
        }

        _playersInfo = new PlayerInfoCollection(player1Info, player2Info, player3Info, player4Info);
    }
    
    public void Turn(Field field)
    {
        Task.Run(() => TurnInternal(field));
    }

    private void TurnInternal(Field field)
    {
        _playersTurn.Clear();
        
        CancellationTokenSource cancellationTokenSource = 
            Players.Players.Any(x=>x.IsDebugMode) ?
                new CancellationTokenSource( ) :
                new CancellationTokenSource(NetworkGameSettings.TurnTimeout);
        
        var parallelOptions = new ParallelOptions
        {
            CancellationToken = cancellationTokenSource.Token
        };

        Parallel.ForEach(Players.Players, parallelOptions,
            (playerBot, _, index) => BotTurn(playerBot, index, field));

        if (!_playersTurn.IsEmpty)
        {
            _turnAction?.Invoke(
                _playersTurn
                    .ToArray()
                    .ToDictionary(
                        x=>(int)(x.Key + 1), 
                        x=>x.Value));
        }
    }

    private void BotTurn(IPlayer playerBot, long index, Field field)
    {
        try
        {
            var playersInfo = PlayersInfo;
            var gameInfo = GameInfo.Create(field, playersInfo.PlayerInfos);
            _playersTurn[index] = playerBot.Turn(gameInfo, playersInfo.PlayerInfos[index]);
        }
        catch (OperationCanceledException)
        {
            playerBot.OnTurnTimeExceeded();
        }
    }
}