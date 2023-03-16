using BombermanGame.Shared.Enums;

namespace BombermanGame.Game.Extensions
{
    public static class FieldItemEnumExtension
    {
        public static FieldItemEnum RemovePlayerFromFieldItem(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.Player1:
                case FieldItemEnum.Player2:
                case FieldItemEnum.Player3:
                case FieldItemEnum.Player4:
                    return FieldItemEnum.EmptyField;
                case FieldItemEnum.Player1WithBomb:
                case FieldItemEnum.Player2WithBomb:
                case FieldItemEnum.Player3WithBomb:
                case FieldItemEnum.Player4WithBomb:
                    return FieldItemEnum.Bomb;
                default:
                    return fieldItemEnum;
            }
        }
        
        public static FieldItemEnum RemoveBombFromFieldItem(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.Bomb:
                    return FieldItemEnum.EmptyField;
                case FieldItemEnum.Player1WithBomb:
                    return FieldItemEnum.Player1;
                case FieldItemEnum.Player2WithBomb:
                    return FieldItemEnum.Player2;
                case FieldItemEnum.Player3WithBomb:
                    return FieldItemEnum.Player3;
                case FieldItemEnum.Player4WithBomb:
                    return FieldItemEnum.Player4;
                default:
                    return fieldItemEnum;
            }
        }

        public static FieldItemEnum AddBombToFieldItem(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.EmptyField:
                    return FieldItemEnum.Bomb;
                case FieldItemEnum.Player1:
                    return FieldItemEnum.Player1WithBomb;
                case FieldItemEnum.Player2:
                    return FieldItemEnum.Player2WithBomb;
                case FieldItemEnum.Player3:
                    return FieldItemEnum.Player3WithBomb;
                case FieldItemEnum.Player4:
                    return FieldItemEnum.Player4WithBomb;
                default:
                    return fieldItemEnum;
            }
        }

        public static bool IsBombOnField(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.Bomb:
                case FieldItemEnum.Player1WithBomb:
                case FieldItemEnum.Player2WithBomb:
                case FieldItemEnum.Player3WithBomb:
                case FieldItemEnum.Player4WithBomb:
                    return true;
                default:
                    return false;
            }
        }
    }
}