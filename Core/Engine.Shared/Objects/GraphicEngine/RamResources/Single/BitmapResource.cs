using Vortice.Direct2D1;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single
{
    public class BitmapResource : BaseRamSingleResource
    {
        public static int ResourceGroupId = 3;
        public override string ResourceName { get; }
        public override IDisposable Resource { get; }
        public override int GetResourceGroupId() => ResourceGroupId;

        public BitmapResource(string resourceName, ID2D1Bitmap bitmap)
        {
            ResourceName = resourceName;
            Resource = bitmap;
        }
    }
}