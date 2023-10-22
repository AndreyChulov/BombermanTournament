using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Contracts.Messages;

public class TurnBotMessage : BaseMessage
{
    public const string MessageString = "TurnBot";
    public PlayerTurnEnum PlayerTurn { get; set; }

    public TurnBotMessage() : this(PlayerTurnEnum.None) {}

    public TurnBotMessage(PlayerTurnEnum playerTurn)
    {
        Message = MessageString;
        PlayerTurn = playerTurn;
    }

    public static TurnBotMessage Initialize(PlayerTurnEnum playerTurn) => new(playerTurn);
}