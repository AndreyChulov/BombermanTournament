using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;

namespace Games.BombermanGame.Shared.RamResources.Multi
{
    public class ScoresResource :BaseRamMultiResource
    {
        public static int ResourceGroupId = 2001;

        public override string ResourceName { get; }
        public TextFormatResource ScoresTextFormatResource { get; }
        public BrushResource ScoresForegroundBrushResource { get; }
        public BrushResource ScoresShadowBrushResource { get; }
        public BrushResource Player1BackgroundBrushResource { get; }
        public BrushResource Player2BackgroundBrushResource { get; }
        public BrushResource Player3BackgroundBrushResource { get; }
        public BrushResource Player4BackgroundBrushResource { get; }
        
        public override List<IDisposable> AssociatedResources { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public ScoresResource(string resourceName, 
            TextFormatResource scoresTextFormatResource, 
            BrushResource scoresForegroundBrushResource, 
            BrushResource scoresShadowBrushResource, 
            BrushResource player1BackgroundBrushResource, 
            BrushResource player2BackgroundBrushResource, 
            BrushResource player3BackgroundBrushResource, 
            BrushResource player4BackgroundBrushResource)
        {
            ResourceName = resourceName;
            
            ScoresTextFormatResource = scoresTextFormatResource;
            ScoresForegroundBrushResource = scoresForegroundBrushResource;
            ScoresShadowBrushResource = scoresShadowBrushResource;
            Player1BackgroundBrushResource = player1BackgroundBrushResource;
            Player2BackgroundBrushResource = player2BackgroundBrushResource;
            Player3BackgroundBrushResource = player3BackgroundBrushResource;
            Player4BackgroundBrushResource = player4BackgroundBrushResource;


            AssociatedResources = new List<IDisposable>
            {
                scoresTextFormatResource,
                scoresForegroundBrushResource,
                scoresShadowBrushResource,
                player1BackgroundBrushResource,
                player2BackgroundBrushResource,
                player3BackgroundBrushResource,
                player4BackgroundBrushResource
            };
        }
    }
}