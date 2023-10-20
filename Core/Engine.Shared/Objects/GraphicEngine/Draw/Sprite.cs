using System.Drawing;
using System.Reflection;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using BitmapInterpolationMode = Vortice.Direct2D1.BitmapInterpolationMode;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public class Sprite :BaseDraw
    {
        protected override string LinkedResourceName => "Sprite";
        protected override int LinkedResourceGroupId => BrushResource.ResourceGroupId;
        
        protected virtual string EmbeddedImageResourceName => "Engine.Shared.GraphicEngine.ImageResources.tree.jpg";
        protected virtual Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 

        private ID2D1Bitmap? _bitmap;
        private readonly RectangleF _drawRectangle;
       
        protected override void SetRamResource(IRamResource resource)
        {
            _bitmap = (ID2D1Bitmap) (((BitmapResource) resource).Resource);
        }

        public static Sprite CreateInPercents(IEngine engine, RectangleF drawRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Sprite(new Rectangle(
                    (int)(drawRectangle.X * canvasWidth),
                    (int)(drawRectangle.Y * canvasHeight),
                    (int)(drawRectangle.Width * canvasWidth),
                    (int)(drawRectangle.Height * canvasHeight)
                )
            );
        }

        protected Sprite(Rectangle drawRectangle)
        {
            _drawRectangle = drawRectangle;
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
        {
            return new BitmapResource(
                LinkedResourceName, 
                renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(EmbeddedImageAssembly, EmbeddedImageResourceName)
                );
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (_bitmap != null)
            {
                renderTarget.DrawBitmap(_bitmap, _drawRectangle, 
                    1f, BitmapInterpolationMode.Linear,
                    null);
            }
        }
    }
}