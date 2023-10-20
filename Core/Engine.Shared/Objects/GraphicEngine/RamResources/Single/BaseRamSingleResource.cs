using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;

namespace Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single
{
    public abstract class BaseRamSingleResource :IRamSingleResource
    {
        public abstract string ResourceName { get; }
        public abstract IDisposable Resource { get; }
        public abstract int GetResourceGroupId();
        
        public void Dispose()
        {
            Resource.Dispose();
        }
    }
}