using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.ObsoleteGame.DrawDataModel.Draw.Score
{
    [Obsolete]
    public class Player3Score : BasePlayerScore
    {
        protected override string LinkedResourceName => "Game.BombermanGame.Score.Player3Score";
        protected override int LinkedResourceGroupId => SystemTextWithShadowResource.ResourceGroupId;
        protected override int PlayerNubmer => 3;

        public static Player3Score Create(IEngine engine, IPlayerInfo playerInfo)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Player3Score(
                playerInfo,
                new Rectangle(
                    (int) (0.15f * canvasWidth),
                    (int) (0.06f * canvasHeight),
                    (int) (0.35f * canvasWidth),
                    (int) (0.04f * canvasHeight)
                ), canvasSize
            );
        }
        
        public Player3Score(IPlayerInfo playerInfo, Rectangle drawRectangle, Size canvasSize) 
            : base(playerInfo, drawRectangle, canvasSize)
        {
        }
    }
}