using System;
using System.Collections.Generic;
using Vortice.Direct2D1;

namespace Engine.SharedInterfaces.GraphicEngine.RamResources
{
    public interface IRamMultiResource: IRamResource
    {
        List<IDisposable> AssociatedResources { get; }
    }
}