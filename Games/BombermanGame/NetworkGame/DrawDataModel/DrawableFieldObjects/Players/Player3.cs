using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.Shared.DrawDataModel.Field;
using Games.BombermanGame.Shared.Interfaces;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.DrawableFieldObjects.Players;

public class Player3 : BasePlayer
{
    public Player3(RectangleF drawRectangle, IPlayer player) : base(drawRectangle, player) { }
    
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player3)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetAssembly(typeof(Player3)) ?? Assembly.GetCallingAssembly(),
                EmbeddedResourceNames.PLAYER_3_0)
        );
    }

    public override void SetFieldResource(FieldResource resource)
    {
        PlayerBitmap = (ID2D1Bitmap)resource.Player3.Resource;
        
        base.SetFieldResource(resource);
    }
}