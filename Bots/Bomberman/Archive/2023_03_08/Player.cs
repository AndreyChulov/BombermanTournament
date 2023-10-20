using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_08
{
    public class Player : IPlayer
    {
        public string Nickname => "Mikhail the Random Idiot";
        public string StrategyDescription => "Do random shit like an idiot";
        public bool IsDebugMode => false;

        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            Random random = new Random();
            return (PlayerTurnEnum)random.Next(6);
        }

        public void OnTurnTimeExceeded()
        {
           
        }
    }
}