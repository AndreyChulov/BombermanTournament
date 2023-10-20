
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_26
{
    public class BraveBraveSirRobin : IPlayer
    {
        public string Nickname => "Brave Brave Sir Robin";
        public string StrategyDescription => "Simple and very courageous strategy";
        public bool IsDebugMode => false;
        public string AiDevelopedForGame => "Bomberman";
        
        static FieldItemEnum[] danger = new FieldItemEnum[] 
        { 
            FieldItemEnum.Bomb,
            FieldItemEnum.Player1WithBomb,
            FieldItemEnum.Player2WithBomb,
            FieldItemEnum.Player3WithBomb,
            FieldItemEnum.Player4WithBomb
        };

        static FieldItemEnum[] passable = new FieldItemEnum[] 
        { 
            FieldItemEnum.EmptyField
        };

        static FieldItemEnum[] target = new FieldItemEnum[] 
        { 
            FieldItemEnum.Player1,
            FieldItemEnum.Player2,
            FieldItemEnum.Player3,
            FieldItemEnum.Player4,
            FieldItemEnum.DestructibleField
        };

        FieldItemEnum[] getAdjascent(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            FieldItemEnum[] adjascent = new FieldItemEnum[] { };
            if (currentPlayerInfo.X + 1 < gameInfo.Field[0].Length)
            {
                adjascent = adjascent.Append(gameInfo.Field[currentPlayerInfo.X + 1][currentPlayerInfo.Y]).ToArray();
            }

            if (currentPlayerInfo.X - 1 >= 0)
            {
                adjascent=adjascent.Append(gameInfo.Field[currentPlayerInfo.X - 1][currentPlayerInfo.Y]).ToArray();
            }

            if (currentPlayerInfo.Y + 1 < gameInfo.Field.Length)
            {
                adjascent=adjascent.Append(gameInfo.Field[currentPlayerInfo.X][currentPlayerInfo.Y + 1]).ToArray();
            }

            if (currentPlayerInfo.Y - 1 >= 0)
            {
                adjascent=adjascent.Append(gameInfo.Field[currentPlayerInfo.X][currentPlayerInfo.Y - 1]).ToArray();
            }
            return adjascent;
        }

        PlayerTurnEnum[] getMoves(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            PlayerTurnEnum[] moves = new PlayerTurnEnum[] { };
            if (currentPlayerInfo.X + 1 < gameInfo.Field[0].Length &&
                passable.Contains(gameInfo.Field[currentPlayerInfo.X + 1][currentPlayerInfo.Y])
                )
            {
                moves = moves.Append(PlayerTurnEnum.MoveRight).ToArray();
            }
            if (currentPlayerInfo.X - 1 >= 0 &&
                passable.Contains(gameInfo.Field[currentPlayerInfo.X - 1][currentPlayerInfo.Y]))
            {
                moves = moves.Append(PlayerTurnEnum.MoveLeft).ToArray();
            }
            if (currentPlayerInfo.Y + 1 < gameInfo.Field.Length &&
                passable.Contains(gameInfo.Field[currentPlayerInfo.X][currentPlayerInfo.Y + 1]))
            {
                moves = moves.Append(PlayerTurnEnum.MoveDown).ToArray();
            }
            if (currentPlayerInfo.Y - 1 >= 0 &&
                passable.Contains(gameInfo.Field[currentPlayerInfo.X][currentPlayerInfo.Y - 1]))
            {
                moves=moves.Append(PlayerTurnEnum.MoveUp).ToArray();
            }
            return moves;
        }

        PlayerTurnEnum BravelyRunAway(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            Random random = new Random();
            var moves = getMoves(gameInfo, currentPlayerInfo);
            if (moves.Length == 0)
            {
                return PlayerTurnEnum.None;
            }
            return moves[random.Next(moves.Length)];
        }

        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            FieldItemEnum myCell = gameInfo.Field[currentPlayerInfo.X][currentPlayerInfo.Y];
            FieldItemEnum[] adjascent = getAdjascent(gameInfo, currentPlayerInfo);

            if (danger.Contains(myCell) ||
                adjascent.Intersect(danger).Any())
            {
                return BravelyRunAway(gameInfo, currentPlayerInfo);
            }
            if (adjascent.Intersect(target).Any())
            {
                return PlayerTurnEnum.PutBomb;
            }
            return BravelyRunAway(gameInfo, currentPlayerInfo);
        }

        public void OnTurnTimeExceeded()
        {
            
        }
    }
}