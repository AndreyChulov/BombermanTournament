using Core.Engine.Shared.Interfaces.GraphicEngine.Draw;
using Core.Engine.Shared.Interfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public abstract class BaseDraw : IDraw
    {
        protected abstract string LinkedResourceName { get; }
        protected abstract int LinkedResourceGroupId { get; }
        
        public IRamResource? Resource { private get; set; }
        public void CreateResource(
            ID2D1HwndRenderTarget renderTarget, 
            IResourcesContainer actualResources,
            IDWriteFactory directWriteFactory)
        {
            IRamResource? resource = actualResources.SearchResource(LinkedResourceGroupId, LinkedResourceName);
            if (resource == null)
            {
                Resource = CreateIRamResource(renderTarget, directWriteFactory);
                SetRamResource(Resource);
                actualResources.AddResource(Resource);
            }
            else
            {
                Resource = resource;
                SetRamResource(Resource);
            }
        }

        protected abstract void SetRamResource(IRamResource resource);
        protected abstract IRamResource CreateIRamResource(ID2D1HwndRenderTarget renderTarget,
            IDWriteFactory directWriteFactory);

        public abstract void Draw(ID2D1HwndRenderTarget renderTarget);
    }
}