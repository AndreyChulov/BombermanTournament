namespace Core.Network.Shared.Contracts.Messages;

public class GetClientInfoMessage : BaseMessage
{
    public const string MessageString = "GetClientInfo";
    
    public static GetClientInfoMessage Initialize() => new GetClientInfoMessage() { Message = MessageString };
}