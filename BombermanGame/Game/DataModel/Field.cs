using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game.DataModel
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
                    var player1Count = _field.Sum(
                        x => x.Count(
                            y => y == FieldItemEnum.Player1 || y == FieldItemEnum.Player1WithBomb)
                    );
                    if (
                        player1Count == 1 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player1 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player1WithBomb
                    )
                    {
                        return true;
                    }
                    break;
                case FieldItemEnum.Player2:
                case FieldItemEnum.Player2WithBomb:
                    var player2Count = _field.Sum(
                        x => x.Count(
                            y => y == FieldItemEnum.Player2 || y == FieldItemEnum.Player2WithBomb)
                    );
                    if (
                        player2Count == 1 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player2 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player2WithBomb
                    )
                    {
                        return true;
                    }
                    break;
                case FieldItemEnum.Player3:
                case FieldItemEnum.Player3WithBomb:
                    var player3Count = _field.Sum(
                        x => x.Count(
                            y => y == FieldItemEnum.Player3 || y == FieldItemEnum.Player3WithBomb)
                    );
                    if (
                        player3Count == 1 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player3 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player3WithBomb
                    )
                    {
                        return true;
                    }
                    break;
                case FieldItemEnum.Player4:
                case FieldItemEnum.Player4WithBomb:
                    var player4Count = _field.Sum(
                        x => x.Count(
                            y => y == FieldItemEnum.Player4 || y == FieldItemEnum.Player4WithBomb)
                    );
                    if (
                        player4Count == 1 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player4 &&
                        _field[lineIndex][columnIndex] != FieldItemEnum.Player4WithBomb
                    )
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public Point GetPlayerStartPosition(int playerIndex)
        {
            FieldItemEnum itemToSearch = 
                playerIndex == 0 ? FieldItemEnum.Player1StartPoint :
                    playerIndex == 1 ? FieldItemEnum.Player2StartPoint :
                    playerIndex == 2 ? FieldItemEnum.Player3StartPoint :
                    FieldItemEnum.Player4StartPoint;
            
            for (int rowIndex = 0; rowIndex < _fieldHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _fieldWidth; columnIndex++)
                {
                    if (_field[rowIndex][columnIndex] == itemToSearch)
                    {
                        switch (playerIndex)
                        {
                            case 0:
                                _field[rowIndex][columnIndex] = FieldItemEnum.Player1;
                                break;
                            case 1:
                                _field[rowIndex][columnIndex] = FieldItemEnum.Player2;
                                break;
                            case 2:
                                _field[rowIndex][columnIndex] = FieldItemEnum.Player3;
                                break;
                            case 3:
                                _field[rowIndex][columnIndex] = FieldItemEnum.Player4;
                                break;
                            default:
                                break;
                        }
                        return new Point(columnIndex, rowIndex);
                    }
                }
            }
            
            return Point.Empty;
        }
    }
}