using Games.BombermanGame.Shared.DrawDataModel.Scores;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel.Score;

public class Player4Score : BasePlayerScore
{
    public override void SetFieldResource(ScoresResource resource)
    {
        BackgroundBrush = (ID2D1Brush)resource.Player4BackgroundBrushResource.Resource;
        
        base.SetFieldResource(resource);
    }

    public Player4Score(RectangleF drawRectangle, float fontSize, PlayerInfoCollection playerInfoCollection) 
        : base(drawRectangle, fontSize, playerInfoCollection.Player4Info)
    {
    }
}