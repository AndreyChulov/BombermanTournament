using System.Drawing;
using Engine.SharedInterfaces;

namespace BombermanGame.DrawDataModel.Draw
{
    public class FpsMeter : Engine.Shared.GraphicEngine.Draw.FpsMeter
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
                ), canvasSize 
            );
        }

        private FpsMeter(Rectangle drawRectangle, Size canvasSize) 
            : base(drawRectangle, canvasSize)
        {
        }
    }
}