using Vortice.DirectWrite;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single
{
    public class TextFormatResource :BaseRamSingleResource
    {
        public static int ResourceGroupId = 2;
        public override string ResourceName { get; }
        public override IDisposable Resource { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public TextFormatResource(string resourceName, IDWriteTextFormat textFormat)
        {
            ResourceName = resourceName;
            Resource = textFormat;
        }
    }
}