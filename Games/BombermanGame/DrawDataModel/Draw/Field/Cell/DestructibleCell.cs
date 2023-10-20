using System.Reflection;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace Games.BombermanGame.DrawDataModel.Draw.Field.Cell;

public static class DestructibleCell
{
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(DestructibleCell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.destructibleCell.png")
        );
    }
}