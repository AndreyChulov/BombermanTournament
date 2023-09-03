namespace Core.Network.ExternalShared.Contracts.Messages;

public class PingMessage : BaseMessage
{
    public const string MessageString = "Ping";
    
    public static PingMessage Initialize() => new PingMessage() { Message = MessageString };
}