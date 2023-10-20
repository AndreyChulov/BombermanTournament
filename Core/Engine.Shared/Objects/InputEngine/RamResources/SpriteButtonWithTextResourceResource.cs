using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi;
using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace Core.Engine.Shared.Objects.InputEngine.RamResources;

public class SpriteButtonWithTextResourceResource : SystemTextResource
{
    public static readonly int ResourceGroupId = 2001;
    
    public override List<IDisposable> AssociatedResources { get; }

    public ID2D1Bitmap BackgroundBitmap { get; }
    
    
    public SpriteButtonWithTextResourceResource(string resourceName, TextFormatResource textFormatResource, BrushResource textBrushResource, ID2D1Bitmap backgroundBitmap) 
        : base(resourceName, textFormatResource, textBrushResource)
    {
        BackgroundBitmap = backgroundBitmap;
        
        AssociatedResources = new List<IDisposable>
        {
            textFormatResource, 
            textBrushResource,
            backgroundBitmap
        };
    }
}