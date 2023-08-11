using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;

namespace Engine.Shared.InputEngine.RamResources;

public class SpriteButtonWithShadowedTextResource : SystemTextWithShadow
{
    public static readonly int ResourceGroupId = 2002;
    
    public override List<IDisposable> AssociatedResources { get; }

    public ID2D1Bitmap BackgroundBitmap { get; }
    
    
    public SpriteButtonWithShadowedTextResource(string resourceName,
        TextFormat textFormat, Brush textBrush, Brush shadowBrush,
        ID2D1Bitmap backgroundBitmap) 
        : base(resourceName, textFormat, shadowBrush, textBrush)
    {
        BackgroundBitmap = backgroundBitmap;
        
        AssociatedResources = new List<IDisposable>
        {
            textFormat, 
            shadowBrush,
            textBrush,
            backgroundBitmap
        };
    }
}