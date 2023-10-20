using System.Drawing;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public class RectangleFilled : BaseDraw
    {
        protected override string LinkedResourceName => "RoundedRectBrush";
        protected override int LinkedResourceGroupId => BrushResource.ResourceGroupId;

        private readonly RectangleF _targetRectangle;

        private ID2D1SolidColorBrush? _brush;

        public static RectangleFilled CreateInPercents(
            IEngine engine, RectangleF targetRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new RectangleFilled(
                new RectangleF(
                    targetRectangle.X * canvasWidth,
                    targetRectangle.Y * canvasHeight,
                    targetRectangle.Width * canvasWidth,
                    targetRectangle.Height * canvasHeight
                )
            );
        }

        protected RectangleFilled(RectangleF targetRectangle)
        {
            _targetRectangle = targetRectangle;
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
                renderTarget.FillRectangle(_targetRectangle, _brush);
            }
        }
    }
}