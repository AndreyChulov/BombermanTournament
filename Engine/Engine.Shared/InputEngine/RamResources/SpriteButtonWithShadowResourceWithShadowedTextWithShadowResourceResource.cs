using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace Engine.Shared.InputEngine.RamResources;

public class SpriteButtonWithShadowResourceWithShadowedTextWithShadowResourceResource : SystemTextWithShadowResource
{
    public static readonly int ResourceGroupId = 2002;
    
    public override List<IDisposable> AssociatedResources { get; }

    public ID2D1Bitmap BackgroundBitmap { get; }
    
    
    public SpriteButtonWithShadowResourceWithShadowedTextWithShadowResourceResource(string resourceName,
        TextFormatResource textFormatResource, BrushResource textBrushResource, BrushResource shadowBrushResource,
        ID2D1Bitmap backgroundBitmap) 
        : base(resourceName, textFormatResource, shadowBrushResource, textBrushResource)
    {
        BackgroundBitmap = backgroundBitmap;
        
        AssociatedResources = new List<IDisposable>
        {
            textFormatResource, 
            shadowBrushResource,
            textBrushResource,
            backgroundBitmap
        };
    }
}