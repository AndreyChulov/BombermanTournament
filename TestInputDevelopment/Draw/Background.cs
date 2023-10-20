using System.Reflection;
using Core.Engine.Shared.Interfaces;
using Image = Core.Engine.Shared.Objects.GraphicEngine.Draw.Image;

namespace TestInputDevelopment.Draw;

public class Background : Image
{
    protected override string LinkedResourceName => "Image.Background";
    protected override string EmbeddedImageResourceName => "TestInputDevelopment.Images.background.jpg";
    protected override Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 
    
    public static Background CreateInstance(IEngine engine)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new Background(new Rectangle(0, 0, canvasWidth, canvasHeight));
    }
    
    protected Background(Rectangle drawRectangle) : base(drawRectangle)
    {
    }
}