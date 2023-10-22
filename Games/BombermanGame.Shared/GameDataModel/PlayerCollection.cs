using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel
{
    public class PlayerCollection
    {
        public IPlayer Player1 { get; }
        public IPlayer Player2 { get; }
        public IPlayer Player3 { get; }
        public IPlayer Player4 { get; }
        
        public PlayerCollection(IPlayer player1, IPlayer player2, IPlayer player3, IPlayer player4)
        {
            Player1 = player1;
            Player2 = player2;
            Player3 = player3;
            Player4 = player4;
        }

        public int GetCurrentPlayerIndex(IPlayer currentPlayer)
        {
            return
                currentPlayer == Player1 ? 0 :
                currentPlayer == Player2 ? 1 :
                currentPlayer == Player3 ? 2 : 3;
        }
    }
}