using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel;

public struct FieldCell : IPositionItem
{
    public int X { get; }
    public int Y { get; }
    public FieldItemEnum Cell { get; }
    
    public FieldCell(int x, int y, FieldItemEnum cell)
    {
        X = x;
        Y = y;
        Cell = cell;
    }
}