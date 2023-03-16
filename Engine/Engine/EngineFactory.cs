using System.Windows.Forms;
using Engine.Graphic;
using Engine.Graphic.Draw;
using Engine.Graphic.RamResources;
using Engine.SharedInterfaces;

namespace Engine
{
    public static class EngineFactory
    {
        public static IEngine CreateEngine(Form form, float dxCanvasSizeFactor)
        {
            var resourcesContainer = new ResourcesContainer();
            var drawContainer = new DrawContainer();
            
            var graphicEngine = GraphicEngineFactory.CreateGraphicEngine(
                form, dxCanvasSizeFactor, resourcesContainer, drawContainer);
                
            return new Engine(graphicEngine, resourcesContainer, drawContainer);
        }
        
    }
}