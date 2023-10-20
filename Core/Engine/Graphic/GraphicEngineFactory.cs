using Core.Engine.Shared.Interfaces.GraphicEngine;
using Core.Engine.Shared.Interfaces.GraphicEngine.Draw;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;

namespace Core.Engine.Graphic
{
    public static class GraphicEngineFactory
    {
        public static IGraphicEngine CreateGraphicEngine(Form form, float dxCanvasSizeFactor,
            IResourcesContainer resourcesContainer, IDrawContainer drawContainer)
        {
            return new GraphicEngineForForm(
                form.Handle, form.Size, dxCanvasSizeFactor, 
                resourcesContainer, drawContainer);
        }
        
    }
}