using Core.Network.Shared.Contracts.Messages;
using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Contracts.Messages;

public class BotCommandMessage : BaseMessage
{
    public const string MessageString = "BotCommand";
    
    public PlayerTurnEnum Command { get; set; }

    public BotCommandMessage() : this(PlayerTurnEnum.None) {}

    private BotCommandMessage(PlayerTurnEnum playerCommand)
    {
        Message = MessageString;
        Command = playerCommand;
    }

    public static BotCommandMessage Initialize(PlayerTurnEnum playerTurn) => new(playerTurn);
}