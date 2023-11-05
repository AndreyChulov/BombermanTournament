using Games.BombermanGame.Shared.DrawDataModel.Field;
using Vortice.Direct2D1;

namespace Games.BombermanGame.Shared.DrawDataModel.Helpers;

public static class PlayerDrawHelper
{
    private static RectangleF GetNicknameForegroundRectangle(RectangleF targetRectangle, float nicknameFontSize)
    {
        return targetRectangle with
        {
            Y = targetRectangle.Y - nicknameFontSize,
            Height = nicknameFontSize + 10
        };
    }

    private static RectangleF GetNicknameShadowRectangle(RectangleF targetRectangle, float nicknameFontSize)
    {
        return targetRectangle with
        {
            X = targetRectangle.X + 5,
            Y = targetRectangle.Y - nicknameFontSize + 5,
            Height = nicknameFontSize + 10
        };
    }

    public static void Draw(ID2D1HwndRenderTarget renderTarget, BasePlayer player)
    {
        BitmapDrawHelper.Draw(renderTarget, player.PlayerBitmap, player.DrawRectangle);
        TextDrawHelper.Draw(renderTarget, 
            GetNicknameShadowRectangle(player.DrawRectangle, player.NicknameFontSize), 
            player.Player.Nickname,
            player.NicknameShadowBrush,
            player.NicknameTextFormat);
        TextDrawHelper.Draw(renderTarget, 
            GetNicknameForegroundRectangle(player.DrawRectangle, player.NicknameFontSize), 
            player.Player.Nickname,
            player.NicknameForegroundBrush,
            player.NicknameTextFormat);
    }
}