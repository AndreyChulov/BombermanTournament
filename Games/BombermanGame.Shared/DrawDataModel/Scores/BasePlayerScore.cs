using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Games.BombermanGame.Shared.Interfaces;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.Shared.DrawDataModel.Scores;

public abstract class BasePlayerScore : IDrawableScoreObject
{
    private readonly IPlayerInfo _playerInfo;
    
    public RectangleF DrawRectangle { get; }
    public float ScoreFontSize { get; }
    public ID2D1Brush? ScoreForegroundBrush { get; set; }
    public ID2D1Brush? ScoreShadowBrush { get; set; }
    public ID2D1Brush? BackgroundBrush { get; protected set; } = null;
    public IDWriteTextFormat? ScoreTextFormat { get; set; }
    public string ScoreText { get; private set; }

    private string GetScoreText() => $"({_playerInfo.Nickname}) scores : {_playerInfo.Score}";
    
    protected BasePlayerScore(RectangleF drawRectangle, float scoreFontSize, IPlayerInfo playerInfo)
    {
        _playerInfo = playerInfo;
        
        _playerInfo.OnScoreUpdated = OnScoreUpdated;
        
        DrawRectangle = drawRectangle;
        ScoreFontSize = scoreFontSize;
        
        OnScoreUpdated();
    }

    private void OnScoreUpdated()
    {
        ScoreText = GetScoreText();
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        ScoreDrawHelper.Draw(renderTarget, this);
    }

    public virtual void SetFieldResource(ScoresResource resource)
    {
        ScoreForegroundBrush = (ID2D1Brush)resource.ScoresForegroundBrushResource.Resource;
        ScoreShadowBrush = (ID2D1Brush)resource.ScoresShadowBrushResource.Resource;
        ScoreTextFormat = (IDWriteTextFormat)resource.ScoresTextFormatResource.Resource;
    }
}