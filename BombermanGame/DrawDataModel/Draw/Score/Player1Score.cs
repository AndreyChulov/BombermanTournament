using System.Drawing;
using BombermanGame.Shared.Interfaces;
using Engine.SharedInterfaces;
using Engine.Shared.GraphicEngine.RamResources.Multi;

namespace BombermanGame.DrawDataModel.Draw.Score
{
    public class Player1Score : BasePlayerScore
    {
        protected override string LinkedResourceName => "BombermanGame.Score.Player1Score";
        protected override int LinkedResourceGroupId => SystemText.ResourceGroupId;
        protected override int PlayerNo => 1;

        public static Player1Score Create(IEngine engine, IPlayerInfo playerInfo)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Player1Score(
                playerInfo,
                new Rectangle(
                    (int) (0.15f * canvasWidth),
                    (int) (0.01f * canvasHeight),
                    (int) (0.35f * canvasWidth),
                    (int) (0.04f * canvasHeight)
                ), canvasSize
            );
        }

        public Player1Score(IPlayerInfo playerInfo, Rectangle drawRectangle, Size canvasSize)
            : base(playerInfo, drawRectangle, canvasSize)
        {
        }
    }
}