using System.Drawing;
using Engine.SharedInterfaces;
using Engine.SharedInterfaces.GraphicEngine;
using Engine.SharedInterfaces.GraphicEngine.Draw;
using Engine.SharedInterfaces.GraphicEngine.RamResources;

namespace Engine
{
    public class Engine: IEngine
    {
        private readonly IGraphicEngine _graphicEngine;
        private readonly IResourcesContainer _resourcesContainer;
        private readonly IDrawContainer _drawContainer;

        public Engine(IGraphicEngine graphicEngine, 
            IResourcesContainer resourcesContainer, IDrawContainer drawContainer)
        {
            _graphicEngine = graphicEngine;
            _resourcesContainer = resourcesContainer;
            _drawContainer = drawContainer;
        }

        public Size GetCanvasSize()
        {
            return _graphicEngine.CanvasSize;
        }

        public void LoadResources()
        {
            _drawContainer.LoadLinkedResources(
                _graphicEngine.HwndRenderTarget, _resourcesContainer, _graphicEngine.DirectWriteFactory);
        }

        public void LoadDrawObjects(IDraw[] drawObjects)
        {
            foreach (var drawObject in drawObjects)
            {
                LoadDrawObject(drawObject);
            }
        }

        public void LoadDrawObject(IDraw drawObject)
        {
            _drawContainer.AddDrawObject(drawObject);
        }

        public void Start()
        {
            _graphicEngine.Start();
        }

        public void Stop()
        {
            _graphicEngine.Stop();
        }

        public void Dispose()
        {
            _graphicEngine.Dispose();
            _resourcesContainer.Dispose();
        }
    }
}