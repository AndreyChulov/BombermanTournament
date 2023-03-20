namespace BombermanGame.DrawDataModel.Draw.Field;

public class FieldCellsGrid
{
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
        _fieldItemsTargetRects = CreateFieldItemsTargetRects(targetRectangle, columnsCount, rowsCount);
    }

    public RectangleF GetCellRectangle(int column, int row) => _fieldItemsTargetRects[row][column];
    
}