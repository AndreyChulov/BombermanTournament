using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Contracts.Messages.DataModel;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.Contracts.Messages;

public class TurnInfoMessage : BaseMessage
{
    public const string MessageString = "TurnInfo";

    public GameInfoContract GameInfo { get; set; } 
    public PlayerInfoContract CurrentPlayerInfo { get; set; }

    public TurnInfoMessage() : this(new GameInfoContract(), new PlayerInfoContract()) {}

    private TurnInfoMessage(GameInfoContract gameInfo, PlayerInfoContract currentPlayerInfo)
    {
        GameInfo = gameInfo;
        CurrentPlayerInfo = currentPlayerInfo;
        Message = MessageString;
    }

    public static TurnInfoMessage Initialize(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo) 
        => new(
            GameInfoContract.Initialize(gameInfo),
            PlayerInfoContract.Initialize(currentPlayerInfo));
}