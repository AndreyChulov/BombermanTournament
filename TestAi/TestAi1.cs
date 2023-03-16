using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace TestAi
{
    public class TestAi1 : IPlayer
    {
        public string Nickname { get; } = "Test";
        public string StrategyDescription { get; } = "only left";
        public bool IsDebugMode { get; } = false;

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