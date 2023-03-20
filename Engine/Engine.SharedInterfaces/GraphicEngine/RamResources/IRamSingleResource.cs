namespace Engine.SharedInterfaces.GraphicEngine.RamResources
{
    public interface IRamSingleResource: IRamResource
    {
        IDisposable Resource { get; }
    }
}