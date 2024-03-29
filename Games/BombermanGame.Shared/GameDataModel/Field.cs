using Games.BombermanGame.Shared.Delegates;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Extensions;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel;

public class Field
{
    public int FieldWidth { get; }
    public int FieldHeight { get; }

    private readonly FieldItemEnum[][] _field;

    public FieldItemEnum[][] GetFieldCloned() =>
        _field.Select(x => (FieldItemEnum[])x.Clone()).ToArray();

    public FieldItemEnum[][] GetField() => _field;

    public Action? OnFieldUpdated { private get; set; } = null;

    public Field(int fieldWidth, int fieldHeight)
    {
        FieldWidth = fieldWidth;
        FieldHeight = fieldHeight;

        _field = CreateField();
    }

    private FieldItemEnum[][] CreateField()
    {
        FieldItemEnum[][] field = new FieldItemEnum[FieldHeight][];

        for (int count = 0; count < FieldHeight; count++)
        {
            field[count] = new FieldItemEnum[FieldWidth];
        }

        return field;
    }

    public FieldItemEnum? GetDownFieldItem(IPositionItem currentPosition, int length = 1)
    {
        return GetDownFieldCell(currentPosition, length)?.Cell;
    }

    private FieldCell? GetDownFieldCell(IPositionItem currentPosition, int length = 1)
    {
        if (currentPosition.Y + length >= FieldHeight)
        {
            return null;
        }

        return  new FieldCell(currentPosition.X, currentPosition.Y + length, 
            _field[currentPosition.Y + length][currentPosition.X]);
    }

    public FieldItemEnum? GetUpFieldItem(IPositionItem currentPosition, int length = 1)
    {
        return GetUpFieldCell(currentPosition, length)?.Cell;
    }

    private FieldCell? GetUpFieldCell(IPositionItem currentPosition, int length = 1)
    {
        if (currentPosition.Y - length < 0)
        {
            return null;
        }

        return  new FieldCell(currentPosition.X, currentPosition.Y - length, 
            _field[currentPosition.Y - length][currentPosition.X]);
    }

    public FieldItemEnum? GetLeftFieldItem(IPositionItem currentPosition, int length = 1)
    {
        return GetLeftFieldCell(currentPosition, length)?.Cell;
    }

    private FieldCell? GetLeftFieldCell(IPositionItem currentPosition, int length = 1)
    {
        if (currentPosition.X - length < 0)
        {
            return null;
        }

        return  new FieldCell(currentPosition.X - length, currentPosition.Y, 
            _field[currentPosition.Y][currentPosition.X - length]);
    }

    public FieldItemEnum? GetRightFieldItem(IPositionItem currentPosition, int length = 1)
    {
        return GetRightFieldCell(currentPosition, length)?.Cell;
    }

    private FieldCell? GetRightFieldCell(IPositionItem currentPosition, int length = 1)
    {
        if (currentPosition.X + length >= FieldWidth)
        {
            return null;
        }

        return  new FieldCell(currentPosition.X + length, currentPosition.Y, 
            _field[currentPosition.Y][currentPosition.X + length]);
    }

    public FieldItemEnum GetCurrentFieldItem(IPositionItem currentPosition)
    {
        return GetCurrentFieldCell(currentPosition).Cell;
    }

    private FieldCell GetCurrentFieldCell(IPositionItem currentPosition)
    {
        return  new FieldCell(currentPosition.X, currentPosition.Y, 
                _field[currentPosition.Y][currentPosition.X]);
    }

    public void SetFieldCell(IPositionItem currentPosition, FieldItemEnum cell)
    {
        SetFieldCell(currentPosition.Y, currentPosition.X, cell);
    }

    public void SetFieldCell(int lineIndex, int columnIndex, FieldItemEnum cell)
    {
        if (lineIndex < 0 || lineIndex >= FieldHeight)
        {
            throw new ArgumentException(
                $"{nameof(lineIndex)} should be in range [0, {FieldHeight - 1}] but equal to {lineIndex}");
        }

        if (columnIndex < 0 || columnIndex >= FieldWidth)
        {
            throw new ArgumentException(
                $"{nameof(columnIndex)} should be in range [0, {FieldWidth - 1}] but equal to {columnIndex}");

        }

        if (CheckFieldForDuplicateUsers(lineIndex, columnIndex, cell))
        {
            throw new ArgumentException(
                $" Setting up cell [{lineIndex}][{columnIndex}] to {cell} cause duplicate users on field");
        }

        _field[lineIndex][columnIndex] = cell;
    }

    public void ForceFieldUpdated()
    {
        OnFieldUpdated?.Invoke();
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

            _field[rowIndex][columnIndex] = itemToSearch.GetPlayerFromStartPoint();

            return new Point(columnIndex, rowIndex);

        }) ?? Point.Empty;
    }

    public T? EnumerateField<T>(FieldEnumeratorDelegate<T> fieldEnumeratorDelegate) where T : struct
    {
        for (int rowIndex = 0; rowIndex < FieldHeight; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < FieldWidth; columnIndex++)
            {
                var result = fieldEnumeratorDelegate
                    .Invoke(rowIndex, columnIndex, _field[rowIndex][columnIndex]);

                if (result != null) return result.Value;
            }
        }

        return null;
    }

    public IEnumerable<FieldCell> GetCrossFields(
        IPositionItem position, int crossLength, FieldItemEnum[] breakFields)
    {
        var crossFields = new List<FieldCell>
        {
            GetCurrentFieldCell(position)
        };

        crossFields.AddRange(
            GetDirectionCells(position, crossLength, GetLeftFieldCell, breakFields));
        crossFields.AddRange(
            GetDirectionCells(position, crossLength, GetRightFieldCell, breakFields));
        crossFields.AddRange(
            GetDirectionCells(position, crossLength, GetDownFieldCell, breakFields));
        crossFields.AddRange(
            GetDirectionCells(position, crossLength, GetUpFieldCell, breakFields));

        return crossFields.ToArray();
    }

    private IEnumerable<FieldCell> GetDirectionCells(IPositionItem position, int crossLength,
        Func<IPositionItem, int, FieldCell?> getDirectionFieldFunc,
        FieldItemEnum[] breakFields)
    {
        var directionCells = new List<FieldCell>();

        for (var counter = 1; counter <= crossLength; counter++)
        {
            var directionCell = getDirectionFieldFunc(position, counter);

            if (!directionCell.HasValue ||
                breakFields.Any(x => x == directionCell.Value.Cell))
            {
                break;
            }

            directionCells.Add(directionCell.Value);
        }

        return directionCells;
    }
    
}
    
