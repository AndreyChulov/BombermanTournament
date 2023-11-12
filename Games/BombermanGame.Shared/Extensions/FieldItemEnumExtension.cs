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
        
        public static FieldItemEnum GetPlayerFieldItem(this FieldItemEnum _, 
            int playerIndex, bool isZeroBasedIndex = false)
        {
            if (isZeroBasedIndex)
            {
                playerIndex++;
            }

            return playerIndex switch
            {
                1 => FieldItemEnum.Player1,
                2 => FieldItemEnum.Player2,
                3 => FieldItemEnum.Player3,
                4 => FieldItemEnum.Player4,
                _ => throw new ArgumentException($"Wrong {nameof(playerIndex)}={playerIndex}")
            };
        }
        public static FieldItemEnum RemoveBombFromFieldItem(this FieldItemEnum fieldItemEnum)
        {
            return fieldItemEnum switch
            {
                FieldItemEnum.Bomb => FieldItemEnum.EmptyField,
                FieldItemEnum.Player1WithBomb => FieldItemEnum.Player1,
                FieldItemEnum.Player2WithBomb => FieldItemEnum.Player2,
                FieldItemEnum.Player3WithBomb => FieldItemEnum.Player3,
                FieldItemEnum.Player4WithBomb => FieldItemEnum.Player4,
                _ => fieldItemEnum
            };
        }

        public static FieldItemEnum AddBombToFieldItem(this FieldItemEnum fieldItemEnum)
        {
            return fieldItemEnum switch
            {
                FieldItemEnum.EmptyField => FieldItemEnum.Bomb,
                FieldItemEnum.Player1 => FieldItemEnum.Player1WithBomb,
                FieldItemEnum.Player2 => FieldItemEnum.Player2WithBomb,
                FieldItemEnum.Player3 => FieldItemEnum.Player3WithBomb,
                FieldItemEnum.Player4 => FieldItemEnum.Player4WithBomb,
                _ => fieldItemEnum
            };
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

        public static bool IsPlayerStartPointOnField(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.Player1StartPoint:
                case FieldItemEnum.Player2StartPoint:
                case FieldItemEnum.Player3StartPoint:
                case FieldItemEnum.Player4StartPoint:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPlayerOnField(this FieldItemEnum fieldItemEnum)
        {
            switch (fieldItemEnum)
            {
                case FieldItemEnum.Player1:
                case FieldItemEnum.Player2:
                case FieldItemEnum.Player3:
                case FieldItemEnum.Player4:
                case FieldItemEnum.Player1WithBomb:
                case FieldItemEnum.Player2WithBomb:
                case FieldItemEnum.Player3WithBomb:
                case FieldItemEnum.Player4WithBomb:
                    return true;
                default:
                    return false;
            }
        }
        
        public static bool IsPlayerOnField(this FieldItemEnum fieldItemEnum, int playerIndex, bool isZeroBased = false)
        {
            if (isZeroBased)
            {
                playerIndex++;
            }

            return playerIndex switch
            {
                1 => fieldItemEnum.IsPlayer1OnField(),
                2 => fieldItemEnum.IsPlayer2OnField(),
                3 => fieldItemEnum.IsPlayer3OnField(),
                4 => fieldItemEnum.IsPlayer4OnField(),
                _ => throw new ArgumentException($"Wrong {nameof(playerIndex)}={playerIndex}")
            };
        }
        
        private static bool IsPlayer1OnField(this FieldItemEnum fieldItemEnum)
            => fieldItemEnum is FieldItemEnum.Player1 or FieldItemEnum.Player1WithBomb;
        
        private static bool IsPlayer2OnField(this FieldItemEnum fieldItemEnum)
            => fieldItemEnum is FieldItemEnum.Player2 or FieldItemEnum.Player2WithBomb;
        
        private static bool IsPlayer3OnField(this FieldItemEnum fieldItemEnum)
            => fieldItemEnum is FieldItemEnum.Player3 or FieldItemEnum.Player3WithBomb;
        
        private static bool IsPlayer4OnField(this FieldItemEnum fieldItemEnum)
            => fieldItemEnum is FieldItemEnum.Player4 or FieldItemEnum.Player4WithBomb;
        
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
        
        public static FieldItemEnum GetPlayerFromStartPoint(this FieldItemEnum playerStartPoint)
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