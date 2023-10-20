using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi
{
    public class SystemTextResource : BaseRamMultiResource
    {
        public static readonly int ResourceGroupId = 1000;
        public override string ResourceName { get; }
        public override List<IDisposable> AssociatedResources { get; }
        public BrushResource TextBrushResource { get; }
        public TextFormatResource TextFormatResource { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public SystemTextResource(string resourceName, TextFormatResource textFormatResource, BrushResource textBrushResource)
        {
            ResourceName = resourceName;
            TextBrushResource = textBrushResource;
            TextFormatResource = textFormatResource;
            
            AssociatedResources = new List<IDisposable>
            {
                textFormatResource, 
                textBrushResource
            };
        }
    }
}