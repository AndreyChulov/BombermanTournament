namespace Core.Engine.Shared.Interfaces.GraphicEngine.RamResources
{
    public interface IRamMultiResource: IRamResource
    {
        List<IDisposable> AssociatedResources { get; }
    }
}