using BombermanGame.Shared.Interfaces;
using Engine.SharedInterfaces;
using Engine.Shared.GraphicEngine.RamResources.Multi;

namespace BombermanGame.DrawDataModel.Draw.Score
{
    public class Player4Score : BasePlayerScore
    {
        protected override string LinkedResourceName => "BombermanGame.Score.Player4Score";
        protected override int LinkedResourceGroupId => SystemTextWithShadow.ResourceGroupId;
        protected override int PlayerNubmer => 4;

        public static Player4Score Create(IEngine engine, IPlayerInfo playerInfo)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Player4Score(
                playerInfo,
                new Rectangle(
                    (int) (0.55f * canvasWidth),
                    (int) (0.06f * canvasHeight),
                    (int) (0.35f * canvasWidth),
                    (int) (0.04f * canvasHeight)
                ), canvasSize
            );
        }

        public Player4Score(IPlayerInfo playerInfo, Rectangle drawRectangle, Size canvasSize)
            : base(playerInfo, drawRectangle, canvasSize)
        {
        }
    }
}