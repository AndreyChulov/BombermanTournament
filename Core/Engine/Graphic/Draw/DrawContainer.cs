using Core.Engine.Shared.Interfaces.GraphicEngine.Draw;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Core.Engine.Graphic.Draw
{
    public class DrawContainer : IDrawContainer
    {
        private List<IDraw> _loadedDrawObjects;

        public DrawContainer()
        {
            _loadedDrawObjects = new List<IDraw>();
        }

        public void LoadLinkedResources(
            ID2D1HwndRenderTarget renderTarget,
            IResourcesContainer actualResources, 
            IDWriteFactory directWriteFactory)
        {
            _loadedDrawObjects.ForEach(x=>x.CreateResource(renderTarget, actualResources, directWriteFactory));
        }

        public void AddDrawObject(IDraw drawObject)
        {
            _loadedDrawObjects.Add(drawObject);
        }

        public void DrawAll(ID2D1HwndRenderTarget renderTarget)
        {
            _loadedDrawObjects.ForEach(x=>x.Draw(renderTarget));
        }

        public void Dispose()
        {
            _loadedDrawObjects
                .OfType<IDisposable>()
                .ToList()
                .ForEach(x=>x.Dispose());
            _loadedDrawObjects.Clear();
        }
    }
}