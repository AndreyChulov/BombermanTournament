namespace Engine.SharedInterfaces.GraphicEngine.RamResources
{
    public interface IRamMultiResource: IRamResource
    {
        List<IDisposable> AssociatedResources { get; }
    }
}