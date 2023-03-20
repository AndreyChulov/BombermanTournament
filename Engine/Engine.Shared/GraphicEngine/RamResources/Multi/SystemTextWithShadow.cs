using System;
using System.Collections.Generic;
using Engine.Shared.GraphicEngine.RamResources.Single;

namespace Engine.Shared.GraphicEngine.RamResources.Multi
{
    public class SystemTextWithShadow : SystemText
    {
        public new static readonly int ResourceGroupId = 1001;
        public Brush ShadowBrush { get; }
        public override string ResourceName { get; }
        public override List<IDisposable> AssociatedResources { get; }

        public override int GetResourceGroupId() => ResourceGroupId;

        public SystemTextWithShadow(
            string resourceName, TextFormat textFormat, Brush shadowBrush, Brush textBrush)
            :base(resourceName, textFormat, textBrush)
        {
            ResourceName = resourceName;
            ShadowBrush = shadowBrush;
            
            AssociatedResources = new List<IDisposable>
            {
                textFormat, 
                shadowBrush,
                textBrush
            };
        }
    }
}