using BombermanGame.Shared;
using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace AiPlayer3
{
    public class Player : IPlayer
    {
        public string Nickname => "Player 3";
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