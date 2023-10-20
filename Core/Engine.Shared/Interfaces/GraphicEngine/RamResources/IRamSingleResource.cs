namespace Core.Engine.Shared.Interfaces.GraphicEngine.RamResources
{
    public interface IRamSingleResource: IRamResource
    {
        IDisposable Resource { get; }
    }
}