using System.Drawing;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public class RoundedRectangleFilled : BaseDraw
    {
        protected override string LinkedResourceName => "RoundedRectangleFilled";
        protected override int LinkedResourceGroupId => BrushResource.ResourceGroupId;

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
            _brush = (ID2D1SolidColorBrush) (((BrushResource) resource).Resource);
        }

        protected override IRamResource CreateIRamResource(ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            var solidColorBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Green, 1f));
            
            return new BrushResource(LinkedResourceName, solidColorBrush);
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