using Engine.Shared.GraphicEngine.RamResources.Multi;
using Engine.Shared.GraphicEngine.RamResources.Single;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;

namespace Engine.Shared.InputEngine.RamResources;

public class SpriteButtonWithTextResource : SystemText
{
    public static readonly int ResourceGroupId = 2001;
    
    public override List<IDisposable> AssociatedResources { get; }

    public ID2D1Bitmap BackgroundBitmap { get; }
    
    
    public SpriteButtonWithTextResource(string resourceName, TextFormat textFormat, Brush textBrush, ID2D1Bitmap backgroundBitmap) 
        : base(resourceName, textFormat, textBrush)
    {
        BackgroundBitmap = backgroundBitmap;
        
        AssociatedResources = new List<IDisposable>
        {
            textFormat, 
            textBrush,
            backgroundBitmap
        };
    }
}