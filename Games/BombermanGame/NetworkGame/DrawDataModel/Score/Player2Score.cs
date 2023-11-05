using Games.BombermanGame.Shared.DrawDataModel.Scores;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.Score;

public class Player2Score : BasePlayerScore
{
    public override void SetFieldResource(ScoresResource resource)
    {
        BackgroundBrush = (ID2D1Brush)resource.Player2BackgroundBrushResource.Resource;
        
        base.SetFieldResource(resource);
    }

    public Player2Score(RectangleF drawRectangle, float fontSize, PlayerInfoCollection playerInfoCollection) 
        : base(drawRectangle, fontSize, playerInfoCollection.Player2Info)
    {
    }
}