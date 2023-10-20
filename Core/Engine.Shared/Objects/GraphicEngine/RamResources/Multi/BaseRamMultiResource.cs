using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Multi
{
    public abstract class BaseRamMultiResource : IRamMultiResource
    {
        public abstract string ResourceName { get; }
        public abstract List<IDisposable> AssociatedResources { get; }
        public abstract int GetResourceGroupId();
        
        public void Dispose()
        {
            AssociatedResources.ForEach(x=>x.Dispose());
            AssociatedResources.Clear();
        }
    }
}