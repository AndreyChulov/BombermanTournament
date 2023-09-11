using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace BombermanBotExamples
{
    public class LongRunningAi : IPlayer
    {
        public string Nickname => "LongRunningAi";
        public string StrategyDescription => "AI with long running turn function - should stack on field";
        public bool IsDebugMode => false;

        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            for (int count = 0; count < 500; count++)
            {
                Thread.CurrentThread.Join(1);
            }
            
            return PlayerTurnEnum.None;
        }

        public void OnTurnTimeExceeded()
        {
            
        }
    }
}