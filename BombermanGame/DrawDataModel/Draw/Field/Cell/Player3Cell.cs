using System.Reflection;
using Engine.Shared.GraphicEngine;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace BombermanGame.DrawDataModel.Draw.Field.Cell;

public static class Player3Cell
{
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(Player3Cell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.Robo.p3.png"
            )
        );
    }
}