using Engine.Shared.GraphicEngine.RamResources.Single;

namespace Engine.Shared.GraphicEngine.RamResources.Multi
{
    public class SystemText : BaseRamMultiResource
    {
        public static readonly int ResourceGroupId = 1000;
        public override string ResourceName { get; }
        public override List<IDisposable> AssociatedResources { get; }
        public Brush TextBrush { get; }
        public TextFormat TextFormat { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public SystemText(string resourceName, TextFormat textFormat, Brush textBrush)
        {
            ResourceName = resourceName;
            TextBrush = textBrush;
            TextFormat = textFormat;
            
            AssociatedResources = new List<IDisposable>
            {
                textFormat, 
                textBrush
            };
        }
    }
}