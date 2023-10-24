using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.GameDataModel;

namespace Games.BombermanGame.NetworkGame;

public class LevelGenerator
{
    private const int FIELD_WIDTH = 17;
    private const int FIELD_HEIGHT = 17;
    private const int DESTRUCTIBLE_FIELD_PERCENTAGE = 30;

    Random _randomizer = new Random();
    
    public static LevelGenerator Initialize() => new();

    public Field CreateField() => new Field(FIELD_WIDTH, FIELD_HEIGHT);

    public void FillFieldStartCells(Field field)
    {
        field.EnumerateField((rowIndex, columnIndex, cell) =>
        {
            var cellIndex = (Row: rowIndex, Column: columnIndex);
            
            if (IsLevelIndestructibleCell(rowIndex, columnIndex))
            {
                field.SetFieldCell(rowIndex, columnIndex, FieldItemEnum.IndestructibleField);
                return (int?)null;
            }            
            
            if (IsLevelEmptyCell(rowIndex, columnIndex))
            {
                field.SetFieldCell(rowIndex, columnIndex, FieldItemEnum.EmptyField);
                return (int?)null;
            }            
            
            if (IsLevelDestructibleCell(rowIndex, columnIndex))
            {
                field.SetFieldCell(rowIndex, columnIndex, FieldItemEnum.DestructibleField);
                return (int?)null;
            }

            return (int?)null;
        });
    }

    public void FillPlayersStartPoints(Field field)
    {
        field.SetFieldCell(0, 0, FieldItemEnum.Player1StartPoint);
        field.SetFieldCell(FIELD_HEIGHT - 1, 0, FieldItemEnum.Player2StartPoint);
        field.SetFieldCell(FIELD_HEIGHT - 1, FIELD_WIDTH - 1, FieldItemEnum.Player3StartPoint);
        field.SetFieldCell(0, FIELD_WIDTH - 1, FieldItemEnum.Player4StartPoint);
    }
    
    private bool IsLevelIndestructibleCell(int row, int column) => 
        row % 2 == 1 && column % 2 == 1;

    private bool IsLevelEmptyCell(int row, int column) =>
        (row < 2 && column < 2) ||
        (row > FIELD_HEIGHT - 3 && column < 2) ||
        (row > FIELD_HEIGHT - 3 && column > FIELD_WIDTH - 3) ||
        (row < 2 && column > FIELD_WIDTH - 3);    
    
    private bool IsLevelDestructibleCell(int row, int column) =>
        _randomizer.Next(100) < DESTRUCTIBLE_FIELD_PERCENTAGE;    
    
}
