using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.DumbBot;

public class Bot : IPlayer
{
    public string Nickname => "Player 1";
    public string StrategyDescription => "No strategy";
    public bool IsDebugMode => true;
    public string AiDevelopedForGame => "Bomberman";

    private PlayerTurnEnum _prevousTurn = PlayerTurnEnum.PutBomb;
    
    public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
    {
        _prevousTurn = _prevousTurn == PlayerTurnEnum.PutBomb ? 
                                   PlayerTurnEnum.MoveUp : 
                                   PlayerTurnEnum.PutBomb;
        return _prevousTurn;

    }

    public void OnTurnTimeExceeded()
    {
           Console.WriteLine("Turn time exceeded.");
    }
}