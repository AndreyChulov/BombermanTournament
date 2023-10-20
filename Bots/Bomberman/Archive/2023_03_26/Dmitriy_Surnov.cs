
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Bots.Bomberman.Archive._2023_03_26
{

    internal class Dmittriy : IPlayer
    {
        public string Nickname { get; } = "Dmitriy";

        public string StrategyDescription { get; } = "Rand";

        public bool IsDebugMode { get; } = false;

        private FieldItemEnum _playerBomb = FieldItemEnum.EmptyField;

        void IPlayer.OnTurnTimeExceeded()
        {

        }
        int CheckingCell(int x, int y, IGameInfo gameInfo)
        {
            if (x > -1 && x < 17 && y > -1 && y < 17)
            {
                FieldItemEnum fieldItem = gameInfo.Field[x][y];
                if (fieldItem == FieldItemEnum.IndestructibleField)
                    return -1;
                if (fieldItem == FieldItemEnum.EmptyField)
                    return 1;
                if (fieldItem == FieldItemEnum.DestructibleField)
                    return 2;
                if (fieldItem == FieldItemEnum.Bomb)
                    return 3;
                if (fieldItem == FieldItemEnum.Player1 ||
                    fieldItem == FieldItemEnum.Player2 ||
                    fieldItem == FieldItemEnum.Player3 ||
                    fieldItem == FieldItemEnum.Player4)
                    return 2;
                if (fieldItem == FieldItemEnum.Player1WithBomb ||
                    fieldItem == FieldItemEnum.Player2WithBomb ||
                    fieldItem == FieldItemEnum.Player3WithBomb ||
                    fieldItem == FieldItemEnum.Player4WithBomb)
                    return 3;
                return 0;
            }
            return -1;
        }

        PlayerTurnEnum Hod(int nomer)
        {
            switch (nomer)
            {
                case 0: return PlayerTurnEnum.MoveDown;
                case 1: return PlayerTurnEnum.MoveUp;
                case 2: return PlayerTurnEnum.MoveRight;
                case 3: return PlayerTurnEnum.MoveLeft;
                default: return PlayerTurnEnum.None;
            }
        }

        PlayerTurnEnum Hod(int[] turnEnum)
        {
            for (int i = 0; i < turnEnum.Length; i++)
            {
                if (turnEnum[i] == 1)
                    return Hod(i);
            }
            return PlayerTurnEnum.None;
        }

        bool IsNotBomb(int X, int Y, IGameInfo gameInfo, int sX, int sY)
        {
            if (Y + 1 != sY && X != sX)
            {
                int temp = CheckingCell(Y + 1, X, gameInfo);
                if (temp == 3)
                    return false;
            }
            if (Y - 1 != sY && X != sX)
            {
                int temp = CheckingCell(Y - 1, X, gameInfo);
                if (temp == 3)
                    return false;
            }

            if (Y != sY && X + 1 != sX)
            {
                int temp = CheckingCell(Y, X + 1, gameInfo);
                if (temp == 3)
                    return false;
            }
            if (Y != sY && X - 1 != sX)
            {
                int temp = CheckingCell(Y, X - 1, gameInfo);
                if (temp == 3)
                    return false;
            }
            return true;
        }

        PlayerTurnEnum IPlayer.Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            int currentX = currentPlayerInfo.X;// (2)
            int currentY = currentPlayerInfo.Y;// (1)
            int[] turnEnum = new int[4];
            int countHod = 0;
            if (_playerBomb == FieldItemEnum.EmptyField)
            {
                for (int i = 0; i < gameInfo.PlayersInfos.Length; i++)
                {
                    if (Nickname == gameInfo.PlayersInfos[i].Nickname)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    _playerBomb = FieldItemEnum.Player1WithBomb;
                                    break;
                                }
                            case 1:
                                {
                                    _playerBomb = FieldItemEnum.Player2WithBomb;
                                    break;
                                }
                            case 2:
                                {
                                    _playerBomb = FieldItemEnum.Player3WithBomb;
                                    break;
                                }
                            case 3:
                                {
                                    _playerBomb = FieldItemEnum.Player4WithBomb;
                                    break;
                                }
                        }
                        break;
                    }
                }
            }
            {
                int temp = CheckingCell(currentY + 1, currentX, gameInfo);
                if (temp == 1)
                    if (IsNotBomb(currentX, currentY + 1, gameInfo, currentX, currentY))
                        turnEnum[0] = 1;
                    else turnEnum[0] = 0;
                else turnEnum[0] = temp;
            }
            {
                int temp = CheckingCell(currentY - 1, currentX, gameInfo);
                if (temp == 1)
                    if (IsNotBomb(currentX, currentY - 1, gameInfo, currentX, currentY))
                        turnEnum[1] = 1;
                    else turnEnum[1] = 0;
                else turnEnum[1] = temp;
            }
            {
                int temp = CheckingCell(currentY, currentX + 1, gameInfo);
                if (temp == 1)
                    if (IsNotBomb(currentX + 1, currentY, gameInfo, currentX, currentY))
                        turnEnum[2] = 1;
                    else turnEnum[2] = 0;
                else turnEnum[2] = temp;
            }
            {
                int temp = CheckingCell(currentY, currentX - 1, gameInfo);
                if (temp == 1)
                    if (IsNotBomb(currentX - 1, currentY, gameInfo, currentX, currentY))
                        turnEnum[3] = 1;
                    else turnEnum[3] = 0;
                else turnEnum[3] = temp;
            }
            if (gameInfo.Field[currentY][currentX] == _playerBomb)
                return Hod(turnEnum);
            for (int i = 0; i < turnEnum.Length; i++)
            {
                if (turnEnum[i] == -1 || turnEnum[i] == 0)
                    continue;
                if (turnEnum[i] == 1)
                    countHod++;
                else if (turnEnum[i] == 2)
                    return PlayerTurnEnum.PutBomb;
                else if (turnEnum[i] == 3)
                    return Hod(turnEnum);
            }
            if (countHod == 0)
                return PlayerTurnEnum.None;
            Random random = new();
            int nomer = random.Next(countHod);
            for (int i = 0; i < turnEnum.Length; i++)
            {
                if (turnEnum[i] == 1)
                    if (nomer == 0)
                        return Hod(i);
                    else nomer--;
            }
            return PlayerTurnEnum.None;
        }
    }
}
