using BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game.DataModel
{
    public class PlayerInfo : IPlayerInfo
    {

        public int Score => _score;
        public string Nickname => _nickname;
        public int X => _x;
        public int Y => _y;
        
        private int _score;
        private readonly string _nickname;
        private int _x;
        private int _y;

        private PlayerInfo(int score, string nickname, int x, int y)
        {
            _score = score;
            _nickname = nickname;
            _x = x;
            _y = y;
        }
        
        public PlayerInfo(string nickname, Point playerPosition) 
            : this(0, nickname, playerPosition.X, playerPosition.Y)
        {}

        public void MoveUp()
        {
            _y--;
        }
        
        public void MoveDown()
        {
            _y++;
        }

        public void MoveLeft()
        {
            _x--;
        }

        public void MoveRight()
        {
            _x++;
        }

        public void BlowUpPlayer()
        {
            _score -= 200;
        }

        public void PlayerBlowUpDestroyableCell()
        {
            _score += 50;
        }
    }
}