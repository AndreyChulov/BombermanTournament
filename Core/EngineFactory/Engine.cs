using System.Drawing;
using Core.Engine.Shared.Interfaces;
using Core.Engine.Shared.Interfaces.GraphicEngine;
using Core.Engine.Shared.Interfaces.GraphicEngine.Draw;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Core.Engine.Shared.Interfaces.InputEngine;

namespace Core.EngineFactory
{
    public class Engine: IEngine
    {
        private readonly IGraphicEngine _graphicEngine;
        private readonly IInputEngine? _inputEngine;
        private readonly IResourcesContainer _resourcesContainer;
        private readonly IDrawContainer _drawContainer;
        private readonly IInputContainer? _inputContainer;

        public Engine(IGraphicEngine graphicEngine, 
            IResourcesContainer resourcesContainer, IDrawContainer drawContainer)
        :this(graphicEngine, null, resourcesContainer, drawContainer, null) {}

        public Engine(IGraphicEngine graphicEngine, IInputEngine? inputEngine,
            IResourcesContainer resourcesContainer, IDrawContainer drawContainer, IInputContainer? inputContainer)
        {
            _graphicEngine = graphicEngine;
            _inputEngine = inputEngine;
            _resourcesContainer = resourcesContainer;
            _drawContainer = drawContainer;
            _inputContainer = inputContainer;
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
            _inputContainer?.AddInputObject(drawObject as IInputEngineObject);
        }

        public void Start()
        {
            _graphicEngine.Start();
            _inputEngine?.Start();
        }

        public void Stop()
        {
            _graphicEngine.Stop();
            _inputEngine?.Stop();
        }

        public void Dispose()
        {
            _graphicEngine.Dispose();
            _inputEngine?.Dispose();
            _resourcesContainer.Dispose();
            _inputContainer?.Dispose();
        }
    }
}