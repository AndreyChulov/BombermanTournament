using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace TestAi
{
    public class KursanovaDaria : IPlayer
    {
        public string Nickname { get; } = "Darya Kursanova";
        public string StrategyDescription { get; } = "random move";
        public bool IsDebugMode { get; } = false;

        public PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            int currentX = currentPlayerInfo.X;
            int currentY = currentPlayerInfo.Y;

            Random rand = new Random();
            int direction = rand.Next(1, 10);
            PlayerTurnEnum turn = new PlayerTurnEnum();

            switch (direction)
            {
                case 1: 
                case 2: { currentX -= 1; turn = PlayerTurnEnum.MoveLeft; break; }
                case 3:
                case 4: { currentX += 1; turn = PlayerTurnEnum.MoveRight; break; }
                case 5:
                case 6: { currentY += 1; turn = PlayerTurnEnum.MoveUp; break; }
                case 7: 
                case 8: { currentY -= 1; turn = PlayerTurnEnum.MoveDown; break; }
                case 9: { turn = PlayerTurnEnum.PutBomb; break; }
            }

            if (currentX >= 0 || currentY >= 0 ||
                currentX < gameInfo.FieldWidth - 1 ||
                currentY < gameInfo.FieldHeight - 1)
            {   
                return turn;
            }
            else return PlayerTurnEnum.None;

        }

        public void OnTurnTimeExceeded()
        {

        }

        private void ScanningField(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo)
        {
            for (int i = 0; i < gameInfo.FieldHeight; i++)
            {
                for (int j = 0; j < gameInfo.FieldWidth; j++)
                {
                    if (gameInfo.Field[i][j] == FieldItemEnum.DestructibleField)
                    {
                        //пока допиливаю
                    }
                }
            }
        }

    }
}