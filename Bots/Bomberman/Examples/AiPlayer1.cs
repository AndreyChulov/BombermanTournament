using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Examples
{
    public class AiPlayer1 : IPlayer
    {
        public string Nickname => "Player 1";
        public string StrategyDescription => "No strategy";
        public bool IsDebugMode => false;
        public string AiDevelopedForGame => "Bomberman";
        
        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            return PlayerTurnEnum.None;
        }

        public void OnTurnTimeExceeded()
        {
           
        }
    }
}