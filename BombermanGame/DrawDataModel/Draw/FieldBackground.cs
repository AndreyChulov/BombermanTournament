using System;
using System.Drawing;
using System.Reflection;
using Engine.Shared.GraphicEngine.Draw;
using Engine.SharedInterfaces;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace BombermanGame.DrawDataModel.Draw
{
    public class FieldBackground :Image
    {
        protected override string LinkedResourceName => "BombermanGame.FieldBackground";

        protected override string EmbeddedImageResourceName => 
            "BombermanGame.EmbeddedResources.fieldBackground.png";
        public static FieldBackground Create(IEngine engine)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new FieldBackground(
                new Rectangle(
                    (int) (0.1f * canvasWidth),
                    (int) (0.1f * canvasHeight),
                    (int) (0.8f * canvasWidth),
                    (int) (0.8f * canvasHeight)
                )
            );
        }
        
        private FieldBackground(Rectangle targetRectangle) :base(targetRectangle)
        {
            var assembly = Assembly.GetAssembly(typeof(Background));

            if (assembly == null)
            {
                throw new MissingMemberException("Embedded image assembly not found");
            }

            EmbeddedImageAssembly = assembly;
        }
    }
}