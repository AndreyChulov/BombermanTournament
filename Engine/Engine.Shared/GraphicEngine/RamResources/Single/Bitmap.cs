using Vortice.Direct2D1;

namespace Engine.Shared.GraphicEngine.RamResources.Single
{
    public class Bitmap : BaseRamSingleResource
    {
        public static int ResourceGroupId = 3;
        public override string ResourceName { get; }
        public override IDisposable Resource { get; }
        public override int GetResourceGroupId() => ResourceGroupId;

        public Bitmap(string resourceName, ID2D1Bitmap bitmap)
        {
            ResourceName = resourceName;
            Resource = bitmap;
        }
    }
}