using System.Reflection;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.InputEngine;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace TestInputDevelopment.Input;

public class Button : Image, IInputEngineMouseInteractableObject
{
    protected override string LinkedResourceName => "Input.Button";
    protected override string EmbeddedImageResourceName => "TestInputDevelopment.Images.button.jpg";
    protected override Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 

    public Rectangle ControlRectangle { get; }
    
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

    private Button(Rectangle drawRectangle) : base(drawRectangle)
    {
        ControlRectangle = drawRectangle;
    }

    public void OnClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
        MessageBox.Show("Clicked!!!", "Yooo!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public void OnMouseDown(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
        
    }

    public void OnMouseUp(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
        
    }

    public void OnMouseDoubleClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
        
    }

    public void OnMouseMouseWheel(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
        
    }
}