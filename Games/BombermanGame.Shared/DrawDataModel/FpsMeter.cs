using System.Drawing;
using Core.Engine.Shared.Interfaces;

namespace Games.BombermanGame.Shared.DrawDataModel
{
    public class FpsMeter : Core.Engine.Shared.Objects.GraphicEngine.Draw.FpsMeter
    {
        protected override string LinkedResourceName => "BombermanGame.FpsMeter";

        public static FpsMeter Create(IEngine engine)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new FpsMeter(
                new Rectangle(
                    (int)(0.93f * canvasWidth),
                    (int)(0.02f * canvasHeight),
                    (int)(0.07f * canvasWidth),
                    (int)(0.05f * canvasHeight)
                ), canvasHeight * 0.03f 
            );
        }

        private FpsMeter(Rectangle drawRectangle, float fontSize) 
            : base(drawRectangle, fontSize)
        {
        }
    }
}