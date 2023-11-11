using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel.Player
{
    public class PlayerCollection
    {
        public IPlayer Player1 { get; }
        public IPlayer Player2 { get; }
        public IPlayer Player3 { get; }
        public IPlayer Player4 { get; }

        private readonly Random _randomizer = new();
        
        public PlayerCollection(IPlayer player1, IPlayer player2, IPlayer player3, IPlayer player4)
        {
            Player1 = player1;
            Player2 = player2;
            Player3 = player3;
            Player4 = player4;
        }
        
        public PlayerCollection(IPlayer[] players)
        {
            players = MixPlayers(players);
            Player1 = players[0];
            Player2 = players[1];
            Player3 = players[2];
            Player4 = players[3];
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                yield return Player1;
                yield return Player2;
                yield return Player3;
                yield return Player4;
            }
        }

        public int GetCurrentPlayerIndex(IPlayer currentPlayer)
        {
            return
                currentPlayer == Player1 ? 0 :
                currentPlayer == Player2 ? 1 :
                currentPlayer == Player3 ? 2 : 3;
        }
        
        private IPlayer[] MixPlayers(IPlayer[] players)
        {
            List<KeyValuePair<IPlayer, int>> playerLottery = new List<KeyValuePair<IPlayer, int>>
            {
                new(players[0], _randomizer.Next(100)),
                new(players[1], _randomizer.Next(100)),
                new(players[2], _randomizer.Next(100)),
                new(players[3], _randomizer.Next(100)),
            };
            
            return playerLottery
                    .OrderBy(x=>x.Value)
                    .Select(x => x.Key)
                    .ToArray();
        }
    }
}