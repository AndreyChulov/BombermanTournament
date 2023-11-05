using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.ObsoleteGame.DrawDataModel.Draw.Field.Cell;

[Obsolete]
public class PlayerWithBombCell
{
    public static void Draw(
        ID2D1HwndRenderTarget renderTarget, RectangleF targetRectangle,
        ID2D1Bitmap? playerBitmap, ID2D1Bitmap? bombBitmap, 
        string nickname, float nicknameFontSize,
        ID2D1Brush? nicknameForegroundBrush, ID2D1Brush? nicknameShadowBrush, 
        IDWriteTextFormat? nicknameTextFormat)
    {
        BitmapDrawHelper.Draw(renderTarget, bombBitmap, targetRectangle);
        PlayerCell.Draw(renderTarget, targetRectangle, 
            playerBitmap, nickname, nicknameFontSize, 
            nicknameForegroundBrush, nicknameShadowBrush, nicknameTextFormat);
    }
}