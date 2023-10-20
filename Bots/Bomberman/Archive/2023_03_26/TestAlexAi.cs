
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_26
{
    public class TestAlexAi : IPlayer
    {
        public string Nickname { get; } = "AlexAI";
        public string StrategyDescription { get; } = "Trying to WIN :)";
        public bool IsDebugMode { get; } = false;
        private PlayerTurnEnum PreviosMove { get; set; } = PlayerTurnEnum.None;
        private int Repetitions { get; set; } = 0;
        private List<FieldItemEnum> DestructableItems { get; } = new List<FieldItemEnum> { FieldItemEnum.DestructibleField, FieldItemEnum.Player1, FieldItemEnum.Player1WithBomb,
        FieldItemEnum.Player2, FieldItemEnum.Player2WithBomb, FieldItemEnum.Player3, FieldItemEnum.Player3WithBomb, FieldItemEnum.Player4, FieldItemEnum.Player4WithBomb};

        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            if (gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X] == FieldItemEnum.Player1WithBomb ||
                gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X] == FieldItemEnum.Player2WithBomb ||
                gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X] == FieldItemEnum.Player3WithBomb ||
                gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X] == FieldItemEnum.Player4WithBomb)
            {
                return StepSide(4, currentPlayerInfo.X, currentPlayerInfo.Y, gameInfo.Field);
            }
            return ChooseMove(gameInfo, currentPlayerInfo);
        }

        public void OnTurnTimeExceeded()
        {

        }
        public PlayerTurnEnum ChooseMove(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            foreach (FieldItemEnum destructable in DestructableItems)
            {
                if (currentPlayerInfo.X - 1 >= 0 && destructable == gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X - 1] ||
                    currentPlayerInfo.X + 1 < 17 && destructable == gameInfo.Field[currentPlayerInfo.Y][currentPlayerInfo.X + 1] ||
                    currentPlayerInfo.Y - 1 >= 0 && destructable == gameInfo.Field[currentPlayerInfo.Y - 1][currentPlayerInfo.X] ||
                    currentPlayerInfo.Y + 1 < 17 && destructable == gameInfo.Field[currentPlayerInfo.Y + 1][currentPlayerInfo.X])
                {
                    return PlayerTurnEnum.PutBomb;
                }
            }
            if (Repetitions > 0 && PreviosMove != PlayerTurnEnum.None && CheckMove(PreviosMove, currentPlayerInfo.X, currentPlayerInfo.Y, gameInfo.Field))
            {
                Repetitions -= new Random().Next(1, Repetitions);
                PlayerTurnEnum temp = SeekDestructable(currentPlayerInfo.X, currentPlayerInfo.Y, gameInfo.Field);
                if (temp != PlayerTurnEnum.None)
                {
                    return PreviosMove;
                }
                else return temp;
                
            }
            return SeekDestructable (currentPlayerInfo.X, currentPlayerInfo.Y, gameInfo.Field);
        }
        static private bool CheckMove(PlayerTurnEnum tempMove, int currentPlayerX, int currentPlayerY, FieldItemEnum[][] field)
        {
            switch (tempMove)
            {
                case PlayerTurnEnum.MoveUp:
                    if (currentPlayerY - 1 >= 0 && FieldItemEnum.EmptyField == field[currentPlayerY - 1][currentPlayerX])
                    {
                        return true;
                    }
                    break;
                case PlayerTurnEnum.MoveRight:
                    if (currentPlayerX + 1 < 17 && FieldItemEnum.EmptyField == field[currentPlayerY][currentPlayerX + 1])
                    {
                        return true;
                    }
                    break;
                case PlayerTurnEnum.MoveDown:
                    if (currentPlayerY + 1 < 17 && FieldItemEnum.EmptyField == field[currentPlayerY + 1][currentPlayerX])
                    {
                        return true;
                    }
                    break;
                case PlayerTurnEnum.MoveLeft:
                    if (currentPlayerX - 1 >= 0 && FieldItemEnum.EmptyField == field[currentPlayerY][currentPlayerX - 1])
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        private PlayerTurnEnum StepSide(int side, int currentPlayerX, int currentPlayerY, FieldItemEnum[][] field)
        {
            if (CheckMove((PlayerTurnEnum)side, currentPlayerX, currentPlayerY, field) == true)
            {
                PreviosMove = (PlayerTurnEnum)side;
                Repetitions = new Random().Next(1, 4);
                return (PlayerTurnEnum)side;                
            }
            else return StepSide(new Random().Next(1,5), currentPlayerX, currentPlayerY, field);
        }
        private PlayerTurnEnum SeekDestructable(int currentPlayerX, int currentPlayerY, FieldItemEnum[][] field)
        {
            int xTarget = 0, yTarget = 0;
            int xStart = currentPlayerX, xEnd = currentPlayerX, yStart = currentPlayerY, yEnd = currentPlayerY;
            int tempTarget1 = 0, tempTarget2 = 0;
            do
            {
                if (xStart > 0)
                {
                    xStart--;
                    if (field[currentPlayerY][xStart] == FieldItemEnum.DestructibleField)
                    {
                        tempTarget1 = xStart;
                    }
                }
                if (xEnd < (field.GetLength(0) - 1))
                {
                    xEnd++;
                    if (field[currentPlayerY][xEnd] == FieldItemEnum.DestructibleField)
                    {
                        tempTarget2 = xEnd;
                    }
                }
                if (field[currentPlayerY][xStart] == FieldItemEnum.IndestructibleField || 
                    field[currentPlayerY][xEnd] == FieldItemEnum.IndestructibleField)
                {
                    break;
                }
                if (xStart == 0 && xEnd == (field.GetLength(0) - 1))
                {
                    break;
                }
            } while (tempTarget1 == 0 && tempTarget2 == 0);
            xTarget = FindeCloseDestr(tempTarget1, tempTarget2, currentPlayerX);
            tempTarget1 = 0; tempTarget2 = 0;
            do
            {
                if (yStart > 0)
                {
                    yStart--;
                    if (field[yStart][currentPlayerX] == FieldItemEnum.DestructibleField)
                    {
                        tempTarget1 = yStart;
                    }
                }
                if (yEnd < (field.GetLength(0) - 1))
                {
                    yEnd++;
                    if (field[yEnd][currentPlayerX] == FieldItemEnum.DestructibleField)
                    {
                        tempTarget2 = yEnd;
                    }
                }
                if (field[yStart][currentPlayerX] == FieldItemEnum.IndestructibleField || 
                    field[yEnd][currentPlayerX] == FieldItemEnum.IndestructibleField)
                {
                    break;
                }
                if (yStart == 0 && yEnd == (field.GetLength(0) - 1))
                {
                    break;
                }
            } while (tempTarget1 == 0 && tempTarget2 == 0);
            yTarget = FindeCloseDestr(tempTarget1, tempTarget2, currentPlayerY);
            if (xTarget == 0 && yTarget == 0)
            {
                Repetitions = 3;
                return (PlayerTurnEnum)new Random().Next(1, 5);
            }
            return StepSide(((int)ChooseDirection(xTarget, yTarget, currentPlayerX, currentPlayerY)), 
                currentPlayerX, currentPlayerY, field);
        }
        private int FindeCloseDestr(int lowPos, int upPos, int currPos)
        {
            if (lowPos == 0)
            {
                return upPos;
            }else if (upPos == 0) 
            {
                return lowPos;
            }
            return (currPos - lowPos) < (upPos - currPos) ? lowPos : upPos; 
        }
        private PlayerTurnEnum ChooseDirection(int xDirect, int yDirect, int currentPlayerX, int currentPlayerY)
        {
            PlayerTurnEnum direction = PlayerTurnEnum.None;
            if (xDirect == 0)
            {
                if (yDirect > currentPlayerY)
                {
                    direction = PlayerTurnEnum.MoveDown;
                }
                else direction = PlayerTurnEnum.MoveUp;
            }
            else if (yDirect == 0)
            {
                if (xDirect > currentPlayerX)
                {
                    direction = PlayerTurnEnum.MoveRight;
                }
                else direction = PlayerTurnEnum.MoveLeft;
            }else if (xDirect != 0 && yDirect != 0)
            {
                int tempDirectX = xDirect > currentPlayerX ? xDirect - currentPlayerX : currentPlayerX - xDirect;
                int tempDirectY = yDirect > currentPlayerY ? yDirect - currentPlayerY : currentPlayerY - yDirect;
                if (tempDirectX < tempDirectY)
                {
                    if (xDirect > currentPlayerX)
                    {
                        direction = PlayerTurnEnum.MoveRight;
                    }
                    else direction = PlayerTurnEnum.MoveLeft;
                }
                else
                {
                    if (yDirect > currentPlayerY)
                    {
                        direction = PlayerTurnEnum.MoveDown;
                    }
                    else direction = PlayerTurnEnum.MoveUp;
                }
            }
            return direction;
        }
    }
}