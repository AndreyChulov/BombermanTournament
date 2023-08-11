using System.Reflection;
using Engine.Shared.GraphicEngine;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace BombermanGame.DrawDataModel.Draw.Field;

public static class FieldBackground
{
    public static BitmapResource CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BitmapResource(
            $"{linkedResourceName}.{nameof(FieldBackground)}",
            renderTarget.LoadBitmapFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.fieldBackground.png")
        );
    }
}