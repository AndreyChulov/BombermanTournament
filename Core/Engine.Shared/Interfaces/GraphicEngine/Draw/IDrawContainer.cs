using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Core.Engine.Shared.Interfaces.GraphicEngine.Draw
{
    public interface IDrawContainer :IDisposable
    {
        void LoadLinkedResources(
            ID2D1HwndRenderTarget renderTarget,
            IResourcesContainer actualResources, 
            IDWriteFactory directWriteFactory);
        
        void AddDrawObject(IDraw drawObject);
        void DrawAll(ID2D1HwndRenderTarget renderTarget);
    }
}