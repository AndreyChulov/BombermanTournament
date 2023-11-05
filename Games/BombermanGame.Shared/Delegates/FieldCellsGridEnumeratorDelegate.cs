namespace Games.BombermanGame.Shared.Delegates;

public delegate T? FieldCellsGridEnumeratorDelegate<T>(int rowIndex, int columnIndex, RectangleF cell) where T:struct;