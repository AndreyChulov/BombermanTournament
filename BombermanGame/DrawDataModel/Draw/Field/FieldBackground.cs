using System.Reflection;
using Engine.Shared.GraphicEngine;
using Vortice.Direct2D1;
using Bitmap = Engine.Shared.GraphicEngine.RamResources.Single.Bitmap;

namespace BombermanGame.DrawDataModel.Draw.Field;

public static class FieldBackground
{
    public static Bitmap CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new Bitmap(
            $"{linkedResourceName}.{nameof(FieldBackground)}",
            renderTarget.LoadBitmapFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.fieldBackground.png")
        );
    }
}