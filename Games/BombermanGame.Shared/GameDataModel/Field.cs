using System.Drawing;
using Games.BombermanGame.Shared.Delegates;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel
{
    public class Field
    {
        private readonly int _fieldWidth;
        private readonly int _fieldHeight;
        private readonly FieldItemEnum[][] _field;

        public FieldItemEnum[][] GetFieldCloned() =>
            _field.Select(x => (FieldItemEnum[]) x.Clone()).ToArray();

        public FieldItemEnum[][] GetField() => _field;
        
        public Field(int fieldWidth, int fieldHeight)
        {
            _fieldWidth = fieldWidth;
            _fieldHeight = fieldHeight;
            
            _field = CreateField();
        }

        private FieldItemEnum[][] CreateField()
        {
            FieldItemEnum[][] field = new FieldItemEnum[_fieldHeight][];

            for (int count = 0; count < _fieldHeight; count++)
            {
                field[count] = new FieldItemEnum[_fieldWidth];
            }

            return field;
        }

        public FieldItemEnum? GetDownFieldItem(IPositionItem currentPosition)
        {
            if (currentPosition.Y + 1 == _fieldHeight)
            {
                return null;
            }

            return _field[currentPosition.Y + 1][currentPosition.X];
        }

        public FieldItemEnum? GetUpFieldItem(IPositionItem currentPosition)
        {
            if (currentPosition.Y == 0)
            {
                return null;
            }

            return _field[currentPosition.Y - 1][currentPosition.X];
        }
        
        public FieldItemEnum? GetLeftFieldItem(IPositionItem currentPosition)
        {
            if (currentPosition.X == 0)
            {
                return null;
            }

            return _field[currentPosition.Y][currentPosition.X - 1];
        }
        
        public FieldItemEnum? GetRightFieldItem(IPositionItem currentPosition)
        {
            if (currentPosition.X + 1 == _fieldWidth)
            {
                return null;
            }

            return _field[currentPosition.Y][currentPosition.X + 1];
        }
        
        public FieldItemEnum GetCurrentFieldItem(IPositionItem currentPosition)
        {
            return _field[currentPosition.Y][currentPosition.X];
        }

        public void SetFieldCell(IPositionItem currentPosition, FieldItemEnum cell)
        {
            SetFieldCell(currentPosition.Y, currentPosition.X, cell);
        }
        
        public void SetFieldCell(int lineIndex, int columnIndex, FieldItemEnum cell)
        {
            if (lineIndex < 0 || lineIndex >= _fieldHeight)
            {
                throw new ArgumentException(
                    $"{nameof(lineIndex)} should be in range [0, {_fieldHeight - 1}] but equal to {lineIndex}");
            }

            if (columnIndex < 0 || columnIndex >= _fieldWidth)
            {
                throw new ArgumentException(
                    $"{nameof(columnIndex)} should be in range [0, {_fieldWidth - 1}] but equal to {columnIndex}");
                
            }

            if (CheckFieldForDuplicateUsers(lineIndex, columnIndex, cell))
            {
                throw new ArgumentException(
                    $" Setting up cell [{lineIndex}][{columnIndex}] to {cell} cause duplicate users on field");
            }

            _field[lineIndex][columnIndex] = cell;
        }

        private bool CheckFieldForDuplicateUsers(int lineIndex, int columnIndex, FieldItemEnum cell)
        {
            switch (cell)
            {
                case FieldItemEnum.Player1:
                case FieldItemEnum.Player1WithBomb:
                    if (CheckFieldForDuplicateUser(lineIndex, columnIndex, FieldItemEnum.Player1)) return true;
                    break;
                case FieldItemEnum.Player2:
                case FieldItemEnum.Player2WithBomb:
                    if (CheckFieldForDuplicateUser(lineIndex, columnIndex, FieldItemEnum.Player2)) return true;
                    break;
                case FieldItemEnum.Player3:
                case FieldItemEnum.Player3WithBomb:
                    if (CheckFieldForDuplicateUser(lineIndex, columnIndex, FieldItemEnum.Player3)) return true;
                    break;
                case FieldItemEnum.Player4:
                case FieldItemEnum.Player4WithBomb:
                    if (CheckFieldForDuplicateUser(lineIndex, columnIndex, FieldItemEnum.Player4)) return true;
                    break;
            }

            return false;
        }

        private bool CheckFieldForDuplicateUser(int lineIndex, int columnIndex, FieldItemEnum player)
        {
            var playerWithBomb = player.AddBombToFieldItem();
            var player1Count = _field.Sum(
                x => x.Count(y => y == player || y == playerWithBomb)
            );
            return player1Count == 1 &&
                   _field[lineIndex][columnIndex] != player &&
                   _field[lineIndex][columnIndex] != playerWithBomb;
        }

        public Point GetPlayerStartPosition(int playerIndex)
        {
            FieldItemEnum itemToSearch = FieldItemEnum.EmptyField.GetStartPointByIndex(playerIndex);
            
            return EnumerateField((rowIndex, columnIndex, cell) =>
            {
                if (cell != itemToSearch)
                {
                    return (Point?)null;
                }
                
                _field[rowIndex][columnIndex] = itemToSearch.GetStartPointRelatedPlayer();
                
                return new Point(columnIndex, rowIndex);

            }) ?? Point.Empty;
        }

        public T? EnumerateField<T>(FieldEnumeratorDelegate<T> fieldEnumeratorDelegate) where T:struct
        {
            for (int rowIndex = 0; rowIndex < _fieldHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _fieldWidth; columnIndex++)
                {
                    var result = fieldEnumeratorDelegate
                        .Invoke(rowIndex, columnIndex, _field[rowIndex][columnIndex]);
                    
                    if (result != null) return result.Value;
                }
            }

            return null;
        }
    }
}