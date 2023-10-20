using Vortice.Direct2D1;

namespace Games.BombermanGame.DrawDataModel.Draw.Field.Cell;

public class BitmapCell
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