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
    public class Text : BaseDraw
    {
        private readonly Rectangle _drawRectangle;
        private readonly Size _canvasSize;

        private IDWriteTextFormat? _textFormat;
        private ID2D1Brush? _textForegroundBrush;
        
        public string TextToDraw { get; set; }

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
                    ), canvasSize 
            );
        }

        protected Text(string textToDraw, Rectangle drawRectangle, Size canvasSize)
        {
            _drawRectangle = drawRectangle;
            _canvasSize = canvasSize;//variable is not needed, this code should be refactored
            TextToDraw = textToDraw;
        }
        
        protected override void SetRamResource(IRamResource resource)
        {
            SystemText systemText = (SystemText) resource;
            
            _textFormat = (IDWriteTextFormat)(systemText.TextFormat.Resource);
            _textForegroundBrush = (ID2D1Brush) (systemText.Brush.Resource);
        }

        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            IDWriteTextFormat idWriteTextFormat = directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Normal, FontStyle.Italic, 
                FontStretch.Normal, _canvasSize.Height * 0.03f);
            ID2D1Brush id2D1Brush= renderTarget.CreateSolidColorBrush(new Color4(Color3.Coral, 1f));

            TextFormat textFormat = new("systemTextFormat", idWriteTextFormat);
            Brush backgroundBrush = new("systemTextBrush", id2D1Brush);

            return new SystemText(LinkedResourceName, textFormat, backgroundBrush);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if ((_textFormat != null) && (_textForegroundBrush != null))
            {
                renderTarget.DrawText(TextToDraw, _textFormat, _drawRectangle, _textForegroundBrush);
            }
        }
    }
}