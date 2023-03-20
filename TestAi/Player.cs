using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace AiPlayer1
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