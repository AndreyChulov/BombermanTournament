using Games.BombermanGame.Shared.Enums;

namespace BombermanGame.Game.Extensions
{
    public static class PlayerTurnEnumExtension
    {
        public static bool IsMoveAction(this PlayerTurnEnum playerTurnEnum)
        {
            switch (playerTurnEnum)
            {
                case PlayerTurnEnum.MoveDown:
                case PlayerTurnEnum.MoveUp:
                case PlayerTurnEnum.MoveLeft:
                case PlayerTurnEnum.MoveRight:
                    return true;
                default:
                    return false;
            }
        }
    }
}