using Engine.SharedInterfaces.GraphicEngine.RamResources;

namespace Engine.Graphic.RamResources
{
    public class ResourcesContainer : IResourcesContainer
    {
        private List<IRamResource> _loadedResources;

        public ResourcesContainer()
        {
            _loadedResources = new List<IRamResource>();
        }

        public IRamResource? SearchResource(int resourceGroupId, string resourceName)
        {
            return _loadedResources
                .Where(x => x.GetResourceGroupId() == resourceGroupId)
                .Where(x => x.ResourceName == resourceName)
                .FirstOrDefault((IRamResource?)null);
        }

        public void AddResource(IRamResource resource)
        {
            _loadedResources.Add(resource);
        }
        
        public void Dispose()
        {
            _loadedResources.ForEach(x=>x.Dispose());
            _loadedResources.Clear();
        }
    }
}