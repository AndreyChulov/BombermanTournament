namespace Core.Engine.Shared.Interfaces.GraphicEngine.RamResources
{
    public interface IResourcesContainer :IDisposable
    {
        IRamResource? SearchResource(int resourceGroupId, string resourceName);
        void AddResource(IRamResource resource);
    }
}