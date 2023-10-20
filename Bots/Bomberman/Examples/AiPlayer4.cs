using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Examples
{
    public class AiPlayer4 : IPlayer
    {
        public string Nickname => "Player 4";
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
}