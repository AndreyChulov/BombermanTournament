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

public class SpriteButtonWithShadowedText : TextWithShadow, IInputEngineMouseInteractableObject
{
    protected override string LinkedResourceName => "Input.SpriteButtonWithShadowedText";
    protected override int LinkedResourceGroupId => SpriteButtonWithTextResourceResource.ResourceGroupId;

    protected virtual string EmbeddedImageResourceName => "Engine.Shared.InputEngine.ImageResources.buttonBackground.png";
    
    protected virtual Assembly EmbeddedImageAssembly { get; set; } = Assembly.GetExecutingAssembly(); 

    private ID2D1Bitmap? _backgroundBitmap;

    public RectangleF ControlRectangle { get; }

    public static SpriteButtonWithShadowedText CreateInPercents(IEngine engine, RectangleF drawRectangle, string textToDraw)
    {
        var canvasSize = engine.GetCanvasSize();
        var canvasWidth = canvasSize.Width;
        var canvasHeight = canvasSize.Height;

        return new SpriteButtonWithShadowedText(new Rectangle(
            (int)(drawRectangle.X * canvasWidth),
            (int)(drawRectangle.Y * canvasHeight),
            (int)(drawRectangle.Width * canvasWidth),
            (int)(drawRectangle.Height * canvasHeight)
        ), 
            textToDraw, 
            canvasHeight * 0.03f);
    }

    protected SpriteButtonWithShadowedText(Rectangle drawRectangle, string textToDraw, float fontSize) 
        : base(textToDraw, drawRectangle, fontSize)
    {
        ControlRectangle = drawRectangle;
    }
    
    protected override void SetRamResource(IRamResource resource)
    {
        base.SetRamResource(resource);
        SpriteButtonWithShadowResourceWithShadowedTextWithShadowResourceResource spriteButtonWithShadowResourceWithTextWithShadowResourceResource = (SpriteButtonWithShadowResourceWithShadowedTextWithShadowResourceResource) resource;
            
        _backgroundBitmap = spriteButtonWithShadowResourceWithTextWithShadowResourceResource.BackgroundBitmap;
    }
    
    protected override IRamResource CreateIRamResource(
        ID2D1HwndRenderTarget renderTarget, IDWriteFactory directWriteFactory)
    {
        SystemTextWithShadowResource textWithShadowResourceResource = (SystemTextWithShadowResource)base.CreateIRamResource(renderTarget, directWriteFactory);
        
        return new SpriteButtonWithShadowResourceWithShadowedTextWithShadowResourceResource(
            LinkedResourceName,
            textWithShadowResourceResource.TextFormatResource,
            textWithShadowResourceResource.TextBrushResource,
            textWithShadowResourceResource.ShadowBrushResource,
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