using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Extensions
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
        
        /// <summary>
        /// 0 => FieldItemEnum.Player1StartPoint<br/>
        /// 1 => FieldItemEnum.Player2StartPoint<br/>
        /// 2 => FieldItemEnum.Player3StartPoint<br/>
        /// _ => FieldItemEnum.Player4StartPoint<br/>
        /// </summary>
        /// <returns></returns>
        public static FieldItemEnum GetStartPointByIndex(this FieldItemEnum _, int playerIndex)
        {
            return playerIndex switch
            {
                0 => FieldItemEnum.Player1StartPoint,
                1 => FieldItemEnum.Player2StartPoint,
                2 => FieldItemEnum.Player3StartPoint,
                3 => FieldItemEnum.Player4StartPoint,
                _ => throw new ArgumentException(
                    $"Wrong argument [{nameof(playerIndex)}] value = [{playerIndex}]")
            };
        }        
        
        public static FieldItemEnum GetStartPointRelatedPlayer(this FieldItemEnum playerStartPoint)
        {
            return playerStartPoint switch
            {
                FieldItemEnum.Player1StartPoint => FieldItemEnum.Player1,
                FieldItemEnum.Player2StartPoint => FieldItemEnum.Player2,
                FieldItemEnum.Player3StartPoint => FieldItemEnum.Player3,
                FieldItemEnum.Player4StartPoint => FieldItemEnum.Player4,
                _ => throw new ArgumentException(
                    $"Wrong argument [{nameof(playerStartPoint)}] value = [{playerStartPoint}]")
            };
        }
    }
}