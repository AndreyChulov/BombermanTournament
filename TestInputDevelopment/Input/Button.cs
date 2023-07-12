using System.Reflection;
using Engine.SharedInterfaces;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace TestInputDevelopment.Input;

public class Button : Image
{
    protected override string LinkedResourceName => "Input.Button";
    protected override string EmbeddedImageResourceName => "TestInputDevelopment.Images.button.jpg";
    protected override Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 
    
    public new static Button CreateInPercents(IEngine engine, RectangleF drawRectangle)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new Button(new Rectangle(
                (int)(drawRectangle.X * canvasWidth),
                (int)(drawRectangle.Y * canvasHeight),
                (int)(drawRectangle.Width * canvasWidth),
                (int)(drawRectangle.Height * canvasHeight)
            )
        );
    }
    
    protected Button(Rectangle drawRectangle) : base(drawRectangle)
    {
    }
}