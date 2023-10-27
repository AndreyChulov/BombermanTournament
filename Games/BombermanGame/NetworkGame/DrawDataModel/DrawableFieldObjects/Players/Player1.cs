using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.ObsoleteGame.DrawDataModel.RamResources.Multi;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.Players;

public class Player1 : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    
    private ID2D1Bitmap? _playerBitmap;
    
    public Player1(RectangleF drawRectangle)
    {
        DrawRectangle = drawRectangle;
    }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player1)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetAssembly(typeof(Player1)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.PLAYER_1_0)
        );
    }

    public void SetFieldResource(FieldResource resource)
    {
        _playerBitmap = (ID2D1Bitmap)resource.Player1.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        BitmapCell.Draw(renderTarget, _playerBitmap, DrawRectangle);
    }
}