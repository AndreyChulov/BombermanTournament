using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace Player1DumbBot;

public class Bot : IPlayer
{
    public string Nickname => "Player 1";
    public string StrategyDescription => "No strategy";
    public bool IsDebugMode => false;

    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
        return PlayerTurnEnum.None;
    }

    public void OnTurnTimeExceeded()
    {
           
    }
}