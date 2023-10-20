namespace Core.Network.Shared.Contracts.Messages;

public class ClientInfoMessage : BaseMessage
{
    public const string MessageString = "ClientInfo";
    public string Nickname { get; set; }
    public string StrategyDescription { get; set; }
    public bool IsDebugMode { get; set; }

    public ClientInfoMessage()
    {
        Nickname = "unknown";
        StrategyDescription = "unknown";
        IsDebugMode = false;
    }
    
    private ClientInfoMessage(string nickname, string strategyDescription, bool isDebugMode)
    {
        Message = MessageString;
        Nickname = nickname;
        StrategyDescription = strategyDescription;
        IsDebugMode = isDebugMode;
    }
    
    public static ClientInfoMessage Initialize(string nickname, string strategyDescription, bool isDebugMode) => 
        new ClientInfoMessage(nickname, strategyDescription, isDebugMode);
}