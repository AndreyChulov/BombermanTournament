using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.ObsoleteGame.DrawDataModel.Draw.Field.Cell;

[Obsolete]
public class PlayerCell
{
    public static void Draw(
        ID2D1HwndRenderTarget renderTarget, RectangleF targetRectangle,
        ID2D1Bitmap? playerBitmap, 
        string nickname, float nicknameFontSize,
        ID2D1Brush? nicknameForegroundBrush, ID2D1Brush? nicknameShadowBrush, 
        IDWriteTextFormat? nicknameTextFormat)
    {
        if (playerBitmap == null || nicknameForegroundBrush == null || 
            nicknameShadowBrush == null || nicknameTextFormat == null)
        {
            return;
        }
        
        BitmapDrawHelper.Draw(renderTarget, playerBitmap, targetRectangle);
        renderTarget.DrawText(
            nickname,
            nicknameTextFormat,
            targetRectangle with
            {
                X = targetRectangle.X + 5, 
                Y = targetRectangle.Y - nicknameFontSize + 5, 
                Height = nicknameFontSize + 10
            },
            nicknameShadowBrush
        );
        renderTarget.DrawText(
            nickname,
            nicknameTextFormat,
            targetRectangle with { Y = targetRectangle.Y - nicknameFontSize, Height = nicknameFontSize + 10 },
            nicknameForegroundBrush
        );
    }
}