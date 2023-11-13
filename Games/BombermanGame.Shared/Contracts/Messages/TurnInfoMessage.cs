using System.Text.Json.Serialization;
using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Contracts.Messages.DataModel;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.Contracts.Messages;

public class TurnInfoMessage : BaseMessage
{
    public const string MessageString = "TurnInfo";

    [JsonInclude]
    public GameInfoContract GameInfo { get; set; } 
    public PlayerInfo? CurrentPlayerInfo { get; set; }

    public TurnInfoMessage() : this(new GameInfoContract(), null) {}

    private TurnInfoMessage(GameInfoContract gameInfo, PlayerInfo? currentPlayerInfo)
    {
        GameInfo = gameInfo;
        CurrentPlayerInfo = currentPlayerInfo;
        Message = MessageString;
    }

    public static TurnInfoMessage Initialize(IGameInfo gameInfo, PlayerInfo currentPlayerInfo) 
        => new(GameInfoContract.Initialize(gameInfo), currentPlayerInfo);
}