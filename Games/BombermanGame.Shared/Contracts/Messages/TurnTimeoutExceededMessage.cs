using Core.Network.Shared.Contracts.Messages;

namespace Games.BombermanGame.Shared.Contracts.Messages;

public class TurnTimeoutExceededMessage : BaseMessage
{
    public const string MessageString = "TurnTimeoutExceeded";

    private TurnTimeoutExceededMessage()
    {
        Message = MessageString;
    }
    
    public static TurnTimeoutExceededMessage Initialize() => new();
}