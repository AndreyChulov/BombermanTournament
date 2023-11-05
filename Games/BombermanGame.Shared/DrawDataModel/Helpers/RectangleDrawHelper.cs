using Vortice.Direct2D1;

namespace Games.BombermanGame.Shared.DrawDataModel.Helpers;

public static class RectangleDrawHelper
{
    public static void Draw(ID2D1HwndRenderTarget renderTarget, RectangleF targetRectangle, ID2D1Brush? brush)
    {
        if (brush == null)
        {
            return;
        }
        
        renderTarget.FillRectangle(
            targetRectangle,
            brush
        );
    }
}