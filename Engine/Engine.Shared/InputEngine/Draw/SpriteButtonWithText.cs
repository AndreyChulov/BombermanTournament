using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Engine.Shared.GraphicEngine;
using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.InputEngine.RamResources;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Engine.SharedInterfaces.InputEngine;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Engine.Shared.InputEngine.Draw;

public class SpriteButtonWithText : GraphicEngine.Draw.Text, IInputEngineMouseInteractableObject
{
    protected override string LinkedResourceName => "Input.SpriteButtonWithText";
    protected override int LinkedResourceGroupId => SpriteButtonWithTextResourceResource.ResourceGroupId;

    protected virtual string EmbeddedImageResourceName => "Engine.Shared.InputEngine.ImageResources.buttonBackground.png";
    
    protected virtual Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 

    private ID2D1Bitmap? _backgroundBitmap;

    public RectangleF ControlRectangle { get; }

    public static SpriteButtonWithText CreateInPercents(IEngine engine, RectangleF drawRectangle, string textToDraw)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new SpriteButtonWithText(new Rectangle(
            (int)(drawRectangle.X * canvasWidth),
            (int)(drawRectangle.Y * canvasHeight),
            (int)(drawRectangle.Width * canvasWidth),
            (int)(drawRectangle.Height * canvasHeight)
        ), 
            textToDraw, 
            canvasHeight * 0.03f);
    }

    protected SpriteButtonWithText(Rectangle drawRectangle, string textToDraw, float fontSize) 
        : base(textToDraw, drawRectangle, fontSize)
    {
        ControlRectangle = drawRectangle;
    }
    
    protected override void SetRamResource(IRamResource resource)
    {
        base.SetRamResource(resource);
        SpriteButtonWithTextResourceResource spriteButtonWithTextResourceResource = (SpriteButtonWithTextResourceResource) resource;
            
        _backgroundBitmap = spriteButtonWithTextResourceResource.BackgroundBitmap;
    }
    
    protected override IRamResource CreateIRamResource(
        ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
    {
        SystemTextResource textResourceResource = (SystemTextResource)base.CreateIRamResource(renderTarget, directWriteFactory);
        
        return new SpriteButtonWithTextResourceResource(
            LinkedResourceName,
            textResourceResource.TextFormatResource,
            textResourceResource.TextBrushResource,
            renderTarget.LoadBitmapWithAlphaChannelFromEmbeddedResource(EmbeddedImageAssembly, EmbeddedImageResourceName)
        );
    }

    public override void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        if (_backgroundBitmap != null)
        {
            renderTarget.DrawBitmap(_backgroundBitmap, ControlRectangle, 
                1f, BitmapInterpolationMode.Linear,
                null);
        }
        
        base.Draw(renderTarget);
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