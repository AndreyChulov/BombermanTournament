using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.Shared.DrawDataModel.Helpers;

public static class TextDrawHelper
{
    public static void Draw(
        ID2D1HwndRenderTarget renderTarget, RectangleF targetRectangle,
        string nickname,
        ID2D1Brush? nicknameBrush, 
        IDWriteTextFormat? nicknameTextFormat)
    {
        if (nicknameBrush == null || nicknameTextFormat == null)
        {
            return;
        }
        
        renderTarget.DrawText(
            nickname,
            nicknameTextFormat,
            targetRectangle,
            nicknameBrush
        );
    }
}