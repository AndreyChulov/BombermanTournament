using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.DrawDataModel.Field;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Games.BombermanGame.Shared.Interfaces;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects;

public class BombCell : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    
    private ID2D1Bitmap? _bombCellBitmap;
    
    public BombCell(RectangleF drawRectangle)
    {
        DrawRectangle = drawRectangle;
    }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(BombCell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetAssembly(typeof(BombCell)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.BOMB_CELL)
        );
    }

    public void SetFieldResource(FieldResource resource)
    {
        _bombCellBitmap = (ID2D1Bitmap)resource.Bomb.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        BitmapDrawHelper.Draw(renderTarget, _bombCellBitmap, DrawRectangle);
    }
}