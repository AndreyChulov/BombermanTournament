using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.DrawDataModel.Draw.Score
{
    public class Player2Score : BasePlayerScore
    {
        protected override string LinkedResourceName => "Game.BombermanGame.Score.Player2Score";
        protected override int LinkedResourceGroupId => SystemTextWithShadowResource.ResourceGroupId;
        protected override int PlayerNubmer => 2;

        public static Player2Score Create(IEngine engine, IPlayerInfo playerInfo)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Player2Score(
                playerInfo,
                new Rectangle(
                    (int) (0.55f * canvasWidth),
                    (int) (0.01f * canvasHeight),
                    (int) (0.35f * canvasWidth),
                    (int) (0.04f * canvasHeight)
                ), canvasSize
            );
        }

        public Player2Score(IPlayerInfo playerInfo, Rectangle drawRectangle, Size canvasSize)
            : base(playerInfo, drawRectangle, canvasSize)
        {
        }
    }
}