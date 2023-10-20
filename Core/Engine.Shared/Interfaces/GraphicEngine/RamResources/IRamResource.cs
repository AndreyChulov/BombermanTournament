namespace Core.Engine.Shared.Interfaces.GraphicEngine.RamResources
{
    public interface IRamResource: IDisposable
    {
        string ResourceName { get; }
        int GetResourceGroupId();
    }
}