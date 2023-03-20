using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game
{
    public class DefaultBombermanPlayer : IPlayer
    {
        public string Nickname => "Default";
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