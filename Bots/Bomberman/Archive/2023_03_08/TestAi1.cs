using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_08
{
    public class TestAi1 : IPlayer
    {
        public string Nickname { get; } = "Test";
        public string StrategyDescription { get; } = "only left";
        public bool IsDebugMode { get; } = false;
        public string AiDevelopedForGame => "Bomberman";
        
        private int _turnNo = 0;
        
        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            if (_turnNo++ % 3 == 2)
            {
                return PlayerTurnEnum.PutBomb;
            }
            return PlayerTurnEnum.MoveLeft;
        }

        public void OnTurnTimeExceeded()
        {
            
        }
    }
}