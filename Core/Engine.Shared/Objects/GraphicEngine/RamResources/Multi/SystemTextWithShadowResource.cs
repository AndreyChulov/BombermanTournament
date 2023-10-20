using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi
{
    public class SystemTextWithShadowResource : SystemTextResource
    {
        public new static readonly int ResourceGroupId = 1001;
        public BrushResource ShadowBrushResource { get; }
        public override string ResourceName { get; }
        public override List<IDisposable> AssociatedResources { get; }

        public override int GetResourceGroupId() => ResourceGroupId;

        public SystemTextWithShadowResource(
            string resourceName, TextFormatResource textFormatResource, BrushResource shadowBrushResource, BrushResource textBrushResource)
            :base(resourceName, textFormatResource, textBrushResource)
        {
            ResourceName = resourceName;
            ShadowBrushResource = shadowBrushResource;
            
            AssociatedResources = new List<IDisposable>
            {
                textFormatResource, 
                shadowBrushResource,
                textBrushResource
            };
        }
    }
}