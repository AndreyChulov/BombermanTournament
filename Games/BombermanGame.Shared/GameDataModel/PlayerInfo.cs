using System.Drawing;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel
{
    public class PlayerInfo : BasePlayerInfo, IPlayerInfo
    {

        public PlayerInfo(string nickname, Point playerPosition) 
            : base(nickname, playerPosition)
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
        }

        public override void PlayerBlowUpDestroyableCell()
        {
            Score += 50;
        }
    }
}