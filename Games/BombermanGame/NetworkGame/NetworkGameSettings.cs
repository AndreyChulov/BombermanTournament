namespace Games.BombermanGame.NetworkGame;

public static class NetworkGameSettings
{
    public static readonly TimeSpan TurnTimeoutForBot = TimeSpan.FromSeconds(0.9f);
    public static readonly TimeSpan TurnTimeoutForServer = TimeSpan.FromSeconds(1.2f);
    public static readonly TimeSpan FirstTurnTimeoutForServer = TimeSpan.FromSeconds(1f);
}