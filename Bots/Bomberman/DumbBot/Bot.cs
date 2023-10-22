using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.DumbBot;

public class Bot : IPlayer
{
    public string Nickname => "Player 1";
    public string StrategyDescription => "No strategy";
    public bool IsDebugMode => true;
    public string AiDevelopedForGame => "Bomberman";

    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
        return PlayerTurnEnum.None;
    }

    public void OnTurnTimeExceeded()
    {
           
    }
}