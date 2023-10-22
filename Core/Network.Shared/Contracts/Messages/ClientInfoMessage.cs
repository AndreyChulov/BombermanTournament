namespace Core.Network.Shared.Contracts.Messages;

public class ClientInfoMessage : BaseMessage
{
    public const string MessageString = "ClientInfo";
    public string Nickname { get; set; }
    public string StrategyDescription { get; set; }
    public string DevelopedForGame { get; set; }
    public bool IsDebugMode { get; set; }

    public ClientInfoMessage()
    {
        DevelopedForGame = "unknown";
        Nickname = "unknown";
        StrategyDescription = "unknown";
        IsDebugMode = false;
    }
    
    protected ClientInfoMessage(
        string nickname, string strategyDescription, bool isDebugMode, string developedForGame)
    {
        Message = MessageString;
        Nickname = nickname;
        StrategyDescription = strategyDescription;
        IsDebugMode = isDebugMode;
        DevelopedForGame = developedForGame;
    }
    
    public static ClientInfoMessage Initialize(
        string nickname, string strategyDescription, bool isDebugMode, string developedForGame) =>
            new(nickname, strategyDescription, isDebugMode, developedForGame);
}