using System.Drawing;
using System.Numerics;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Engine.Shared.GraphicEngine.Draw
{
    public class TextWithShadow : Text
    {
        protected Vector2 ShadowPoint { get; }
        
        protected ID2D1Brush? ShadowBrush { get; private set; }
        protected override string LinkedResourceName => "systemTextWithShadow";
        protected override int LinkedResourceGroupId => SystemTextWithShadowResource.ResourceGroupId;

        public new static TextWithShadow CreateInPercents(IEngine engine, string textToDraw, RectangleF drawRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new TextWithShadow(
                    textToDraw, new Rectangle(
                        (int)(drawRectangle.X * canvasWidth),
                        (int)(drawRectangle.Y * canvasHeight),
                        (int)(drawRectangle.Width * canvasWidth),
                        (int)(drawRectangle.Height * canvasHeight)
                    ), 
                    canvasHeight * 0.03f 
            );
        }

        protected TextWithShadow(string textToDraw, Rectangle drawRectangle, float fontSize)
            : base(textToDraw, drawRectangle, fontSize)
        {
            var shadowX = drawRectangle.X + 5;
            var shadowY = drawRectangle.Y + 5;
            
            ShadowPoint = new(shadowX, shadowY);
        }
        
        protected override void SetRamResource(IRamResource resource)
        {
            base.SetRamResource(resource);
            var systemText = (SystemTextWithShadowResource) resource;
            
            ShadowBrush = (ID2D1Brush) (systemText.ShadowBrushResource.Resource);
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            SystemTextResource baseResource = (SystemTextResource)base.CreateIRamResource(renderTarget, directWriteFactory);
            ID2D1Brush id2D1ShadowBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f));

            BrushResource shadowBrushResource = new("systemTextShadowBrush", id2D1ShadowBrush);

            return new SystemTextWithShadowResource(LinkedResourceName, baseResource.TextFormatResource, shadowBrushResource, baseResource.TextBrushResource);
        }
        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (TextLayout == null || TextForegroundBrush == null || ShadowBrush == null)
            {
                return;
            }
            
            renderTarget.DrawTextLayout(ShadowPoint, TextLayout, ShadowBrush);
            
            base.Draw(renderTarget);
        }
    }
}