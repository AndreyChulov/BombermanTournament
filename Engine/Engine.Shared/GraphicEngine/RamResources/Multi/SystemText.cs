using System;
using System.Collections.Generic;
using Engine.Shared.GraphicEngine.RamResources.Single;

namespace Engine.Shared.GraphicEngine.RamResources.Multi
{
    public class SystemText : BaseRamMultiResource
    {
        public static int ResourceGroupId = 1000;
        public override string ResourceName { get; }
        public override List<IDisposable> AssociatedResources { get; }
        public Brush Brush { get; }
        public TextFormat TextFormat { get; }
        
        public override int GetResourceGroupId() => ResourceGroupId;

        public SystemText(string resourceName, TextFormat textFormat, Brush brush)
        {
            ResourceName = resourceName;
            Brush = brush;
            TextFormat = textFormat;
            AssociatedResources = new List<IDisposable>
            {
                textFormat, 
                brush
            };
        }
    }
}