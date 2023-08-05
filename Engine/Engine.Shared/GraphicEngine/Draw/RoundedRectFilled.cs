using System.Drawing;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Brush = Engine.Shared.GraphicEngine.RamResources.Single.Brush;

namespace Engine.Shared.GraphicEngine.Draw
{
    public class RoundedRectangleFilled : BaseDraw
    {
        protected override string LinkedResourceName => "RoundedRectangleFilled";
        protected override int LinkedResourceGroupId => Brush.ResourceGroupId;

        private readonly RoundedRectangle _targetRoundedRectangle;

        private ID2D1SolidColorBrush? _brush;

        public static RoundedRectangleFilled CreateInPercents(
            IEngine engine, RoundedRectangle targetRoundedRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new RoundedRectangleFilled(
                new RoundedRectangle(
                    RectangleF.FromLTRB(
                        targetRoundedRectangle.Rect.Left * canvasWidth,
                        targetRoundedRectangle.Rect.Top * canvasHeight,
                        targetRoundedRectangle.Rect.Right * canvasWidth,
                        targetRoundedRectangle.Rect.Bottom * canvasHeight
                    ),
                    targetRoundedRectangle.RadiusX * canvasWidth,
                    targetRoundedRectangle.RadiusY * canvasHeight
                )
            );
        }
        
        private RoundedRectangleFilled(RoundedRectangle targetRoundedRectangle)
        {
            _targetRoundedRectangle = targetRoundedRectangle;
        }

        protected override void SetRamResource(IRamResource resource)
        {
            _brush = (ID2D1SolidColorBrush) (((Brush) resource).Resource);
        }

        protected override IRamResource CreateIRamResource(ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            var solidColorBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Green, 1f));
            
            return new Brush(LinkedResourceName, solidColorBrush);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (_brush != null)
            {
                renderTarget.FillRoundedRectangle(_targetRoundedRectangle, _brush);
            }
        }
    }
}