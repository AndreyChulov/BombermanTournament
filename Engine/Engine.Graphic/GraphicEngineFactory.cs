using System.Windows.Forms;
using Engine.SharedInterfaces.GraphicEngine;
using Engine.SharedInterfaces.GraphicEngine.Draw;
using Engine.SharedInterfaces.GraphicEngine.RamResources;

namespace Engine.Graphic
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