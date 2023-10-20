using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace Games.BombermanGame.DrawDataModel.Draw.Field.Cell;

public class Player2StartPointCell
{
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player2StartPointCell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.playerBlueStartPoint.png"
            )
        );
    }
}