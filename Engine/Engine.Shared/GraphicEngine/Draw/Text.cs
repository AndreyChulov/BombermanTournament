using System.Drawing;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Brush = Engine.Shared.GraphicEngine.RamResources.Single.Brush;
using FontStyle = Vortice.DirectWrite.FontStyle;

namespace Engine.Shared.GraphicEngine.Draw
{
    public class Text : BaseDraw
    {
        protected Rectangle DrawRectangle { get; }
        protected float FontSize { get; }
        protected IDWriteTextFormat? TextFormat { get; private set; }
        protected ID2D1Brush? TextForegroundBrush { get; private set; }
        protected string TextToDraw { get; set; }

        protected override string LinkedResourceName => "systemText";
        protected override int LinkedResourceGroupId => SystemText.ResourceGroupId;

        public static Text CreateInPercents(IEngine engine, string textToDraw, RectangleF drawRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Text(
                    textToDraw, new Rectangle(
                        (int)(drawRectangle.X * canvasWidth),
                        (int)(drawRectangle.Y * canvasHeight),
                        (int)(drawRectangle.Width * canvasWidth),
                        (int)(drawRectangle.Height * canvasHeight)
                    ),
                    canvasHeight * 0.03f
            );
        }

        protected Text(string textToDraw, Rectangle drawRectangle, float fontSize)
        {
            DrawRectangle = drawRectangle;
            FontSize = fontSize;
            TextToDraw = textToDraw;
        }
        
        protected override void SetRamResource(IRamResource resource)
        {
            SystemText systemText = (SystemText) resource;
            
            TextFormat = (IDWriteTextFormat)(systemText.TextFormat.Resource);
            TextForegroundBrush = (ID2D1Brush) (systemText.TextBrush.Resource);
        }

        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            IDWriteTextFormat idWriteTextFormat = directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Normal, FontStyle.Italic, 
                FontStretch.Normal, FontSize);
            ID2D1Brush id2D1Brush= renderTarget.CreateSolidColorBrush(new Color4(Color3.Coral, 1f));

            TextFormat textFormat = new("systemTextFormat", idWriteTextFormat);
            Brush textBrush = new("systemTextBrush", id2D1Brush);

            return new SystemText(LinkedResourceName, textFormat, textBrush);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if ((TextFormat != null) && (TextForegroundBrush != null))
            {
                renderTarget.DrawText(TextToDraw, TextFormat, DrawRectangle, TextForegroundBrush);
            }
        }
    }
}