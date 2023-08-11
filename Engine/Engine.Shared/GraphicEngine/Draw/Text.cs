using System.Drawing;
using System.Numerics;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using FontStyle = Vortice.DirectWrite.FontStyle;

namespace Engine.Shared.GraphicEngine.Draw
{
    public class Text : BaseDraw
    {
        protected Rectangle DrawRectangle { get; }
        protected Vector2 DrawPoint { get; }
        protected float FontSize { get; }
        protected IDWriteTextFormat? TextFormat { get; private set; }
        protected ID2D1Brush? TextForegroundBrush { get; private set; }

        private string _textToDraw = String.Empty;
        private IDWriteFactory? _directWriteFactory;

        protected override string LinkedResourceName => "systemText";
        protected override int LinkedResourceGroupId => SystemTextResource.ResourceGroupId;

        protected IDWriteTextLayout? TextLayout { get; set; }

        protected string TextToDraw
        {
            get => _textToDraw;
            set
            {
                _textToDraw = value;

                if (TextFormat != null)
                {
                    UpdateTextLayout(TextFormat);    
                }
            }
        }
        
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
            DrawPoint = new Vector2(DrawRectangle.Left, DrawRectangle.Top);
            FontSize = fontSize;
            TextToDraw = textToDraw;
        }
        
        private void UpdateTextLayout(IDWriteTextFormat textFormat)
        {
            TextLayout = _directWriteFactory?.CreateTextLayout(
                TextToDraw, textFormat, DrawRectangle.Width, DrawRectangle.Height);

            if (TextLayout != null)
            {
                TextLayout.TextAlignment = TextAlignment.Center;
                TextLayout.ParagraphAlignment = ParagraphAlignment.Center;
            }
        }

        protected override void SetRamResource(IRamResource resource)
        {
            SystemTextResource systemTextResource = (SystemTextResource) resource;
            
            TextFormat = (IDWriteTextFormat)(systemTextResource.TextFormatResource.Resource);
            TextForegroundBrush = (ID2D1Brush) (systemTextResource.TextBrushResource.Resource);
        }

        protected override IRamResource CreateIRamResource(
            ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory)
        {
            _directWriteFactory = directWriteFactory;
            
            IDWriteTextFormat idWriteTextFormat = directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Normal, FontStyle.Italic, 
                FontStretch.Normal, FontSize);

            UpdateTextLayout(idWriteTextFormat);
            
            ID2D1Brush id2D1Brush= renderTarget.CreateSolidColorBrush(new Color4(Color3.Coral, 1f));

            TextFormatResource textFormatResource = new("systemTextFormat", idWriteTextFormat);
            BrushResource textBrushResource = new("systemTextBrush", id2D1Brush);

            return new SystemTextResource(LinkedResourceName, textFormatResource, textBrushResource);
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            if (TextLayout == null || TextForegroundBrush == null)
            {
                return;
            }

            renderTarget.DrawTextLayout(DrawPoint, TextLayout, TextForegroundBrush);
        }
    }
}