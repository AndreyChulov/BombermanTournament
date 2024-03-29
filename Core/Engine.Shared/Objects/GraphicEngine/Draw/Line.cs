using System.Numerics;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public class Line : BaseDraw
    {
        protected override string LinkedResourceName => "LineBrush";
        protected override int LinkedResourceGroupId => BrushResource.ResourceGroupId;

        private readonly Vector2 _point0;
        private readonly Vector2 _point1;

        private ID2D1SolidColorBrush? _brush;

        public static Line CreateInPercents(IEngine engine, Vector2 point0, Vector2 point1)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Line(
                new Vector2(point0.X * canvasWidth, point0.Y * canvasHeight),
                new Vector2(point1.X * canvasWidth, point1.Y * canvasHeight)
            );
        }

        private Line(Vector2 point0, Vector2 point1)
        {
            _point0 = point0;
            _point1 = point1;
        }

        protected override void SetRamResource(IRamResource resource)
        {
            _brush = (ID2D1SolidColorBrush) (((BrushResource) resource).Resource);
        }

        protected override IRamResource CreateIRamResource(ID2D1HwndRenderTarget renderTarget, IDWriteFactory _)
        {
            var solidColorBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Fuchsia, 1f));
            
            return new BrushResource(LinkedResourceName, solidColorBrush);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (_brush != null)
            {
                renderTarget.DrawLine(_point0, _point1, _brush);
            }
        }
    }
}