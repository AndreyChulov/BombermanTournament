using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Engine.SharedInterfaces.GraphicEngine.Draw
{
    public interface IDraw
    {
        IRamResource? Resource { set; }
        
        void CreateResource(ID2D1HwndRenderTarget renderTarget,
            IResourcesContainer actualResources, IDWriteFactory directWriteFactory);
        
        void Draw(ID2D1HwndRenderTarget renderTarget);
    }
}