using BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game.DataModel
{
    public class Bomb : IPositionItem
    {
        public int TicksToBoom { get; private set; }
        public int X { get; }
        public int Y { get; }
        public IPlayerInfo Owner { get; }

        public Bomb(IPlayerInfo currentPlayerInfo)
        {
            TicksToBoom = 3;
            X = currentPlayerInfo.X;
            Y = currentPlayerInfo.Y;
            Owner = currentPlayerInfo;
        }
        
        public void Tick()
        {
            TicksToBoom--;
        }
    }
}