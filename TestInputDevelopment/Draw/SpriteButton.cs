using System.Reflection;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.InputEngine;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace TestInputDevelopment.Draw;

public class SpriteButton : Engine.Shared.InputEngine.Draw.SpriteButtonWithShadowedText
{
    private readonly string _textToDraw;
    //protected override string LinkedResourceName => "Input.Button";
    //protected override string EmbeddedImageResourceName => "TestInputDevelopment.Images.button.jpg";
    //protected override Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly();

    public new static SpriteButton CreateInPercents(IEngine engine, RectangleF drawRectangle)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new SpriteButton(new Rectangle(
            (int)(drawRectangle.X * canvasWidth),
            (int)(drawRectangle.Y * canvasHeight),
            (int)(drawRectangle.Width * canvasWidth),
            (int)(drawRectangle.Height * canvasHeight)
        ), "Button", drawRectangle.Height * canvasHeight * 0.5f);
    }

    private SpriteButton(Rectangle drawRectangle, string textToDraw, float fontSize) : base(drawRectangle, textToDraw, fontSize)
    {
        _textToDraw = textToDraw;
    }

    public override void OnClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
        MessageBox.Show("Clicked!!!", "Yooo!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

}