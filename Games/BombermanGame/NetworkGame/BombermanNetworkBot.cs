using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;
using TournamentServer.Shared;

namespace Games.BombermanGame.NetworkGame;

public class BombermanNetworkBot : IPlayer
{
    private readonly IConnectedClientInfo _connectedClientInfo;
    public string Nickname { get; }
    public string StrategyDescription { get; }
    public bool IsDebugMode { get; }
    public string AiDevelopedForGame { get; }

    private PlayerTurnEnum? _playerTurn = null;
    private ManualResetEvent _turnResetEvent = new ManualResetEvent(false);

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
            case TurnBotMessage.MessageString:
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
        _turnResetEvent.WaitOne();
        throw new NotImplementedException();
    }

    public void OnTurnTimeExceeded()
    {
        throw new NotImplementedException();
    }
}