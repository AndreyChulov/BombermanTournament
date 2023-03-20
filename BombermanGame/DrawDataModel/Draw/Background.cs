using System.Reflection;
using Engine.SharedInterfaces;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace BombermanGame.DrawDataModel.Draw
{
    public class Background :Image
    {
        protected override string LinkedResourceName => "BombermanGame.Background";
        protected override string EmbeddedImageResourceName => 
            "BombermanGame.EmbeddedResources.background.png";

        public static Background Create(IEngine engine)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new Background(new Rectangle(0, 0, canvasWidth, canvasHeight));
        }

        private Background(Rectangle drawRectangle) : base(drawRectangle)
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