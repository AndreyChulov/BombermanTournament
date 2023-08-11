using System.Reflection;
using Engine.Shared.GraphicEngine;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace BombermanGame.DrawDataModel.Draw.Field.Cell;

public static class Player4Cell
{
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player4Cell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.Robo.p4.png"
            )
        );
    }
}