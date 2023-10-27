using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.ObsoleteGame.DrawDataModel.RamResources.Multi;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.Players;

public class Player4 : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    
    private ID2D1Bitmap? _playerBitmap;
    
    public Player4(RectangleF drawRectangle)
    {
        DrawRectangle = drawRectangle;
    }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player4)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetAssembly(typeof(Player4)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.PLAYER_4_0)
        );
    }

    public void SetFieldResource(FieldResource resource)
    {
        _playerBitmap = (ID2D1Bitmap)resource.Player4.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        BitmapCell.Draw(renderTarget, _playerBitmap, DrawRectangle);
    }
}