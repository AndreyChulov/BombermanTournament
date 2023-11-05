using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel.Player
{
    public class PlayerInfo : BasePlayerInfo
    {

        public PlayerInfo(string nickname, Point playerPosition) 
            : base(nickname, playerPosition)
        {}

        public PlayerInfo(IPlayer player, int x, int y) 
            : base(player.Nickname, new Point(x, y))
        {}
        
        public void MoveUp()
        {
            Y--;
        }
        
        public void MoveDown()
        {
            Y++;
        }

        public void MoveLeft()
        {
            X--;
        }

        public void MoveRight()
        {
            X++;
        }

        public override void BlowUpPlayer()
        {
            Score -= 200;
            
            OnScoreUpdated?.Invoke();
        }

        public override void PlayerBlowUpDestroyableCell()
        {
            Score += 50;
            
            OnScoreUpdated?.Invoke();
        }
    }
}