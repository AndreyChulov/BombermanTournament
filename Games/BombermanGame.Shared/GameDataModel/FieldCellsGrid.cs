using Games.BombermanGame.Shared.Delegates;

namespace Games.BombermanGame.Shared.GameDataModel;

public class FieldCellsGrid
{
    private readonly int _columnsCount;
    private readonly int _rowsCount;
    private readonly RectangleF[][] _fieldItemsTargetRects;

    private static RectangleF[][] CreateFieldItemsTargetRects(
        RectangleF targetRectangle, int columnsCount, int rowsCount)
    {
        var startX = targetRectangle.X;
        var startY = targetRectangle.Y;
        var targetWidth = targetRectangle.Width;
        var targetHeight = targetRectangle.Height;
        var deltaWidth = targetWidth / columnsCount;
        var deltaHeight = targetHeight / rowsCount;

        RectangleF[][] fieldItemsTargetRects = new RectangleF[rowsCount][];

        for (int rowCounter = 0; rowCounter < rowsCount; rowCounter++)
        {
            fieldItemsTargetRects[rowCounter] = new RectangleF[columnsCount];
                
            for (int columnCounter = 0; columnCounter < columnsCount; columnCounter++)
            {
                fieldItemsTargetRects[rowCounter][columnCounter] = 
                    new RectangleF(
                        startX + columnCounter * deltaWidth,
                        startY + rowCounter * deltaHeight,
                        deltaWidth, 
                        deltaHeight
                    );
            }
        }

        return fieldItemsTargetRects;
    }

    public FieldCellsGrid(RectangleF targetRectangle, int columnsCount, int rowsCount)
    {
        _columnsCount = columnsCount;
        _rowsCount = rowsCount;
        _fieldItemsTargetRects = CreateFieldItemsTargetRects(targetRectangle, columnsCount, rowsCount);
    }

    public RectangleF GetCellRectangle(int column, int row) => _fieldItemsTargetRects[row][column];

    public T? EnumerateCells<T>(FieldCellsGridEnumeratorDelegate<T> enumerator) where T:struct
    {
        for (int rowIndex = 0; rowIndex < _rowsCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < _columnsCount; columnIndex++)
            {
                var result = enumerator
                    .Invoke(rowIndex, columnIndex, _fieldItemsTargetRects[rowIndex][columnIndex]);
                    
                if (result != null) return result.Value;
            }
        }

        return null;
    }
}