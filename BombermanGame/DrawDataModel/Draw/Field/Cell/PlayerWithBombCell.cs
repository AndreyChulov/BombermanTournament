using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace BombermanGame.DrawDataModel.Draw.Field.Cell;

public class PlayerWithBombCell
{
    public static void Draw(
        ID2D1HwndRenderTarget renderTarget, RectangleF targetRectangle,
        ID2D1Bitmap? playerBitmap, ID2D1Bitmap? bombBitmap, 
        string nickname, float nicknameFontSize,
        ID2D1Brush? nicknameForegroundBrush, ID2D1Brush? nicknameShadowBrush, 
        IDWriteTextFormat? nicknameTextFormat)
    {
        BitmapCell.Draw(renderTarget, bombBitmap, targetRectangle);
        PlayerCell.Draw(renderTarget, targetRectangle, 
            playerBitmap, nickname, nicknameFontSize, 
            nicknameForegroundBrush, nicknameShadowBrush, nicknameTextFormat);
    }
}