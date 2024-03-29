using Vortice.Direct2D1;

namespace Games.BombermanGame.Shared.DrawDataModel.Helpers;

public static class BitmapDrawHelper
{
    public static void Draw(ID2D1HwndRenderTarget renderTarget, ID2D1Bitmap? bitmap, RectangleF targetRectangle)
    {
        if (bitmap == null)
        {
            return;
        }
        
        renderTarget.DrawBitmap(
            bitmap, 
            targetRectangle,
            1f,
            BitmapInterpolationMode.Linear,
            null
        );
    }
}