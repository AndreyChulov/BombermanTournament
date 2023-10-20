using System.Drawing;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_08
{
    public class StasTest : IPlayer
    {
        public string Nickname { get; } = "TestStas";
        public string StrategyDescription { get; } = "Dumb";
        public bool IsDebugMode { get; } = true;
        public string AiDevelopedForGame => "Bomberman";
        
        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            // Find all destructible fields
            List<Point> destructibleFields = new List<Point>();
            for (int i = 0; i < gameInfo.FieldHeight; i++)
            {
                for (int j = 0; j < gameInfo.FieldWidth; j++)
                {
                    if (gameInfo.Field[i][j] == FieldItemEnum.DestructibleField)
                    {
                        destructibleFields.Add(new Point(j, i));
                    }
                }
            }

            // Sort destructible fields by distance to current player position
            destructibleFields.Sort((p1, p2) =>
            {
                int distance1 = Math.Abs(p1.X - currentPlayerInfo.X) + Math.Abs(p1.Y - currentPlayerInfo.Y);
                int distance2 = Math.Abs(p2.X - currentPlayerInfo.X) + Math.Abs(p2.Y - currentPlayerInfo.Y);
                return distance1.CompareTo(distance2);
            });

            // Move towards the closest destructible field
            foreach (Point destructibleField in destructibleFields)
            {
                if ((destructibleField.X == currentPlayerInfo.X && destructibleField.Y == currentPlayerInfo.Y)||
                    (destructibleField.X+1 == currentPlayerInfo.X && destructibleField.Y == currentPlayerInfo.Y)||
                    (destructibleField.X == currentPlayerInfo.X && destructibleField.Y+1 == currentPlayerInfo.Y)||
                    (destructibleField.X-1 == currentPlayerInfo.X && destructibleField.Y == currentPlayerInfo.Y)||
                    (destructibleField.X == currentPlayerInfo.X && destructibleField.Y-1 == currentPlayerInfo.Y)||
                    (destructibleField.X+1 == currentPlayerInfo.X && destructibleField.Y+1 == currentPlayerInfo.Y))
                {
                    // We reached the destructible field, put a bomb and move to the next one
                    destructibleFields.Clear();

                    return PlayerTurnEnum.PutBomb;
                    
                }

                // Check if there is a bomb nearby
                bool bombNearby = false;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = currentPlayerInfo.X + i;
                        int y = currentPlayerInfo.Y + j;
                        if (x >= 0 && x < gameInfo.FieldWidth && y >= 0 && y < gameInfo.FieldHeight &&
                            gameInfo.Field[y][x] == FieldItemEnum.Bomb)
                        {
                            bombNearby = true;
                            break;
                        }
                    }
                }

                // Move towards the destructible field, avoiding bombs if possible
                if (!bombNearby)
                {
                    if (destructibleField.X < currentPlayerInfo.X)
                    {
                        return PlayerTurnEnum.MoveLeft;
                    }
                    else if (destructibleField.X > currentPlayerInfo.X)
                    {
                        return PlayerTurnEnum.MoveRight;
                    }
                    else if (destructibleField.Y < currentPlayerInfo.Y)
                    {
                        return PlayerTurnEnum.MoveUp;
                    }
                    else if (destructibleField.Y > currentPlayerInfo.Y)
                    {
                        return PlayerTurnEnum.MoveDown;
                    }
                }
                else
                {
                    // Try to move away from the bomb
                    if (currentPlayerInfo.X < gameInfo.FieldWidth - 1 &&
                        gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X + 1] != FieldItemEnum.Bomb)
                    {
                        return PlayerTurnEnum.MoveRight;
                    }
                    else if (currentPlayerInfo.X > 0 && gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X - 1] !=
                             FieldItemEnum.Bomb)
                    {
                        return PlayerTurnEnum.MoveLeft;
                    }
                    else if (currentPlayerInfo.Y < gameInfo.FieldHeight - 1 &&
                             gameInfo.Field[currentPlayerInfo.Y + 1][currentPlayerInfo.X] != FieldItemEnum.Bomb)
                    {
                        return PlayerTurnEnum.MoveDown;
                    }
                    else if (currentPlayerInfo.Y > 0 && gameInfo.Field[currentPlayerInfo.Y - 1][currentPlayerInfo.X] !=
                             FieldItemEnum.Bomb)
                    {
                        return PlayerTurnEnum.MoveUp;
                    }
                }
                destructibleFields.Clear();
            }

            // No more destructible fields to move towards, just wait
            return PlayerTurnEnum.None;
        }

        public void OnTurnTimeExceeded()
        {
            
        }
    }
}