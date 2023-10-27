using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.ObsoleteGame.DrawDataModel.RamResources.Multi;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects;

public class FieldBackground : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    
    private ID2D1Bitmap? _fieldBackgroundBitmap;
    
    public FieldBackground(RectangleF drawRectangle)
    {
        DrawRectangle = drawRectangle;
    }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(FieldBackground)}",
            renderTarget.LoadBitmapFromEmbeddedResource(
                Assembly.GetAssembly(typeof(FieldBackground)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.FIELD_BACKGROUND)
        );
    }

    public void SetFieldResource(FieldResource resource)
    {
        _fieldBackgroundBitmap = (ID2D1Bitmap)resource.FieldBackgroundBitmapResource.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        BitmapCell.Draw(renderTarget, _fieldBackgroundBitmap, DrawRectangle);
    }
}