using Vortice.DirectWrite;

namespace Engine.Shared.GraphicEngine.RamResources.Single
{
    public class TextFormat :BaseRamSingleResource
    {
        public static int ResourceGroupId = 2;
        public override string ResourceName { get; }
        public override IDisposable Resource { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public TextFormat(string resourceName, IDWriteTextFormat textFormat)
        {
            ResourceName = resourceName;
            Resource = textFormat;
        }
    }
}