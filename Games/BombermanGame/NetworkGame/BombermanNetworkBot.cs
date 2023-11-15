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
    private ManualResetEvent _turnResetEvent = 
        new ManualResetEvent(false);

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
                _turnResetEvent.Set();
                throw new NotImplementedException();
                    break;
            default:
                throw new NotImplementedException();
        }
    }

    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
        _connectedClientInfo.SendMessage(
            TurnInfoMessage.Initialize((GameInfo)gameInfo, (PlayerInfo)currentPlayerInfo));
        _turnResetEvent.Reset();

        while (!_turnResetEvent.WaitOne(TimeSpan.FromSeconds(0.1f)))
        {
            if (_isDisposed)
            {
                break;
            }
        }
        throw new NotImplementedException();
    }

    public void OnTurnTimeExceeded()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _isDisposed = true;
        //_turnResetEvent.Set();
        _turnResetEvent.Dispose();
    }
}