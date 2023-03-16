using System;
using Vortice.Direct2D1;

namespace Engine.SharedInterfaces.GraphicEngine.RamResources
{
    public interface IRamSingleResource: IRamResource
    {
        IDisposable Resource { get; }
    }
}