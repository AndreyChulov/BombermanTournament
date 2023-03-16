using System.Drawing;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Engine.Shared.GraphicEngine.Draw
{
    public class RectangleFilled : BaseDraw
    {
        protected override string LinkedResourceName => "RoundedRectBrush";
        protected override int LinkedResourceGroupId => Brush.ResourceGroupId;

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
                renderTarget.FillRectangle(_targetRectangle, _brush);
            }
        }
    }
}