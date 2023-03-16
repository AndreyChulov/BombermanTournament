using System;
using Engine.SharedInterfaces.GraphicEngine.RamResources;

namespace Engine.Shared.GraphicEngine.RamResources.Single
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