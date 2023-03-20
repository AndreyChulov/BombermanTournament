using System.Reflection;
using Engine.Shared.GraphicEngine;
using Vortice.Direct2D1;
using Bitmap = Engine.Shared.GraphicEngine.RamResources.Single.Bitmap;

namespace BombermanGame.DrawDataModel.Draw.Field.Cell;

public class Player3StartPointCell
{
    public static Bitmap CreateBitmapResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new Bitmap(
            $"{linkedResourceName}.{nameof(Player3StartPointCell)}",
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(
                Assembly.GetExecutingAssembly(),
                "BombermanGame.EmbeddedResources.playerRedStartPoint.png"
            )
        );
    }
}