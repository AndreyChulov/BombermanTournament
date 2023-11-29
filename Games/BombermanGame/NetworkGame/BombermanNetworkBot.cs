using System.Runtime.Serialization;
using System.Text.Json;
using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkBot : IPlayer, IDisposable
{
    private readonly IConnectedClientInfo _connectedClientInfo;
    public string Nickname { get; }
    public string StrategyDescription { get; }
    public bool IsDebugMode { get; }
    public string AiDevelopedForGame { get; }

    private PlayerTurnEnum? _playerTurn = null;
    public ParallelLoopState PlayerTurnParallelLoopState { private get; set; }

    private bool _isDisposed = false;

    public BombermanNetworkBot(IConnectedClientInfo connectedClientInfo)
    {
        _connectedClientInfo = connectedClientInfo;
        
        AiDevelopedForGame = _connectedClientInfo.Game;
        
        if (AiDevelopedForGame != "Bomberman")
        {
            throw new ArgumentException(
                $"Connected ai should be developed to [Bomberman] game, but it developed for [{AiDevelopedForGame}]");
        }
        
        _connectedClientInfo.OnMessageReceivedAction = ConnectedClientInfo_OnMessageReceivedAction;

        Nickname = _connectedClientInfo.NickName;
        StrategyDescription = _connectedClientInfo.StrategyDescription;
        IsDebugMode = _connectedClientInfo.IsDebugMode;
    }

    private void ConnectedClientInfo_OnMessageReceivedAction(BaseMessage baseMessage, string serializedMessage)
    {
        switch (baseMessage.Message)
        { 
            case BotCommandMessage.MessageString:
                var botCommand = JsonSerializer.Deserialize<BotCommandMessage>(serializedMessage);
                
                if (botCommand == null)
                {
                    throw new SerializationException($"Can not deserialize [{nameof(BotCommandMessage)}]");
                }
                
                _playerTurn = botCommand.Command;
                    break;
            default:
                throw new NotImplementedException();
        }
    }

    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
        _playerTurn = null;
        
        _connectedClientInfo.SendMessage(
            TurnInfoMessage.Initialize((GameInfo)gameInfo, (PlayerInfo)currentPlayerInfo));

        while (!_playerTurn.HasValue)
        {
            Thread.CurrentThread.Join(TimeSpan.FromSeconds(0.1f));

            if (PlayerTurnParallelLoopState?.ShouldExitCurrentIteration == true)
            {
                throw new OperationCanceledException();
            }
            
            if (_isDisposed)
            {
                break;
            }
        }

        return _playerTurn ?? PlayerTurnEnum.None;
    }

    public void OnTurnTimeExceeded()
    {
        _connectedClientInfo.SendMessage(TurnTimeoutExceededMessage.Initialize());
    }

    public void Dispose()
    {
        _isDisposed = true;
    }
}