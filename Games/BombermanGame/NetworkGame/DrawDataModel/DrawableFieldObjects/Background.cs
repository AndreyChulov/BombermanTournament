using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.DrawDataModel.Field;
using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Games.BombermanGame.Shared.Interfaces;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects;

public class Background : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    
    private ID2D1Bitmap? _fieldBackgroundBitmap;
    
    public Background(RectangleF drawRectangle)
    {
        DrawRectangle = drawRectangle;
    }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Background)}",
            renderTarget.LoadBitmapFromEmbeddedResource(
                Assembly.GetAssembly(typeof(Background)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.FIELD_BACKGROUND)
        );
    }

    public void SetFieldResource(FieldResource resource)
    {
        _fieldBackgroundBitmap = (ID2D1Bitmap)resource.FieldBackgroundBitmapResource.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        BitmapDrawHelper.Draw(renderTarget, _fieldBackgroundBitmap, DrawRectangle);
    }
}