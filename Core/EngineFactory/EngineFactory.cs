using System.Windows.Forms;
using Core.Engine.Graphic;
using Core.Engine.Graphic.Draw;
using Core.Engine.Graphic.RamResources;
using Core.Engine.Input;
using Core.Engine.Shared.Interfaces;

namespace Core.EngineFactory
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
                
                return new Core.EngineFactory.Engine(graphicEngine, inputEngine, resourcesContainer, drawContainer, inputContainer);
            }
            else
            {
                return new Core.EngineFactory.Engine(graphicEngine, resourcesContainer, drawContainer);
            }
        }
        
    }
}