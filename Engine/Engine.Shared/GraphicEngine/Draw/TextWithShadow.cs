using System.Drawing;
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
        protected Rectangle ShadowRectangle { get; }
        
        protected ID2D1Brush? ShadowBrush { get; private set; }
        protected override string LinkedResourceName => "systemTextWithShadow";
        protected override int LinkedResourceGroupId => SystemTextWithShadow.ResourceGroupId;

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
            ShadowRectangle = new Rectangle(
                new Point(
                    drawRectangle.X + 5,
                    drawRectangle.Y + 5
                    ), 
                drawRectangle.Size);
        }
        
        protected override void SetRamResource(IRamResource resource)
        {
            base.SetRamResource(resource);
            var systemText = (SystemTextWithShadow) resource;
            
            ShadowBrush = (ID2D1Brush) (systemText.ShadowBrush.Resource);
        }
        
        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            IDWriteTextFormat idWriteTextFormat = directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Normal, FontStyle.Italic, 
                FontStretch.Normal, FontSize);
            ID2D1Brush id2D1TextBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Coral, 1f));
            ID2D1Brush id2D1ShadowBrush = renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f));

            TextFormat textFormat = new("systemTextFormat", idWriteTextFormat);
            Brush textBrush = new("systemTextBrush", id2D1TextBrush);
            Brush shadowBrush = new("systemTextShadowBrush", id2D1ShadowBrush);

            return new SystemTextWithShadow(LinkedResourceName, textFormat, shadowBrush, textBrush);
        }
        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (TextFormat == null || TextForegroundBrush == null || ShadowBrush == null)
            {
                return;
            }
            
            renderTarget.DrawText(TextToDraw, TextFormat, ShadowRectangle, ShadowBrush);
            renderTarget.DrawText(TextToDraw, TextFormat, DrawRectangle, TextForegroundBrush);
        }
    }
}