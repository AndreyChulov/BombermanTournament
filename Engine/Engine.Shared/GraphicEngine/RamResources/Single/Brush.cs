using System;
using Vortice.Direct2D1;

namespace Engine.Shared.GraphicEngine.RamResources.Single
{
    public class Brush : BaseRamSingleResource
    {
        public static int ResourceGroupId = 1;
        
        public override string ResourceName { get; }
        public override IDisposable Resource { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;
        
        public Brush(string resourceName, ID2D1Brush brush)
        {
            ResourceName = resourceName;
            Resource = brush;
        }
    }
}