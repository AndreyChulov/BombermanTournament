namespace Engine.SharedInterfaces.GraphicEngine.RamResources
{
    public interface IRamResource: IDisposable
    {
        string ResourceName { get; }
        int GetResourceGroupId();
    }
}