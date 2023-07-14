using System.Windows.Forms;
using Engine.Graphic;
using Engine.Graphic.Draw;
using Engine.Graphic.RamResources;
using Engine.Input;
using Engine.SharedInterfaces;

namespace Engine
{
    public static class EngineFactory
    {
        public static IEngine CreateEngine(Form form, float dxCanvasSizeFactor, bool isInputEngineRequired = false)
        {
            var resourcesContainer = new ResourcesContainer();
            var drawContainer = new DrawContainer();
            
            var graphicEngine = GraphicEngineFactory.CreateGraphicEngine(
                form, dxCanvasSizeFactor, resourcesContainer, drawContainer);

            if (isInputEngineRequired)
            {
                var inputContainer = new InputContainer();
                var inputEngine = InputEngineFactory.CreateInputEngine(form, dxCanvasSizeFactor, inputContainer);
                
                return new Engine(graphicEngine, inputEngine, resourcesContainer, drawContainer, inputContainer);
            }
            else
            {
                return new Engine(graphicEngine, resourcesContainer, drawContainer);
            }
        }
        
    }
}