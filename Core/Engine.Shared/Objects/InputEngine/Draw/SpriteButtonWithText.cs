using System.Drawing;
using System.Reflection;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Interfaces.InputEngine;
using Core.Engine.Shared.Objects.GraphicEngine;
using Core.Engine.Shared.Objects.GraphicEngine.Draw;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Core.Engine.Shared.Objects.InputEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Core.Engine.Shared.Objects.InputEngine.Draw;

public class SpriteButtonWithText : Text, IInputEngineMouseInteractableObject
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