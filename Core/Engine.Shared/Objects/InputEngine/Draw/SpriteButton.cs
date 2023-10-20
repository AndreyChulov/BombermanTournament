using System.Drawing;
using System.Reflection;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.InputEngine;
using Core.Engine.Shared.Objects.GraphicEngine.Draw;

namespace Core.Engine.Shared.Objects.InputEngine.Draw;

public class SpriteButton : Sprite, IInputEngineMouseInteractableObject
{
    protected override string LinkedResourceName => "Input.SpriteButton";
    protected override string EmbeddedImageResourceName => "Engine.Shared.InputEngine.ImageResources.buttonBackground.png";
    protected override Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly();

    public RectangleF ControlRectangle { get; }

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
            )
        );
    }

    protected SpriteButton(Rectangle drawRectangle) : base(drawRectangle)
    {
        ControlRectangle = drawRectangle;
    }

    public virtual void OnClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
    }

    public virtual void OnMouseDown(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
    }

    public virtual void OnMouseUp(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
    }

    public virtual void OnMouseDoubleClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
    }

    public virtual void OnMouseMouseWheel(Point mousePosition, MouseEventArgs mouseClickEventArgs)
    {
    }
}