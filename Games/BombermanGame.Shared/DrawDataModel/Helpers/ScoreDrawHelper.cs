using Games.BombermanGame.Shared.DrawDataModel.Field;
using Games.BombermanGame.Shared.DrawDataModel.Scores;
using Vortice.Direct2D1;

namespace Games.BombermanGame.Shared.DrawDataModel.Helpers;

public static class ScoreDrawHelper
{
    private static RectangleF GetScoreForegroundRectangle(RectangleF targetRectangle)
        => targetRectangle;

    private static RectangleF GetScoreShadowRectangle(RectangleF targetRectangle, float nicknameFontSize)
    {
        return targetRectangle with
        {
            X = targetRectangle.X + 5,
            Y = targetRectangle.Y + 5,
        };
    }

    public static void Draw(ID2D1HwndRenderTarget renderTarget, BasePlayerScore playerScore)
    {
        RectangleDrawHelper.Draw(renderTarget, playerScore.DrawRectangle, playerScore.BackgroundBrush);
        TextDrawHelper.Draw(renderTarget, 
            GetScoreShadowRectangle(playerScore.DrawRectangle, playerScore.ScoreFontSize), 
            playerScore.ScoreText,
            playerScore.ScoreShadowBrush,
            playerScore.ScoreTextFormat);
        TextDrawHelper.Draw(renderTarget, 
            GetScoreForegroundRectangle(playerScore.DrawRectangle), 
            playerScore.ScoreText,
            playerScore.ScoreForegroundBrush,
            playerScore.ScoreTextFormat);
    }
}