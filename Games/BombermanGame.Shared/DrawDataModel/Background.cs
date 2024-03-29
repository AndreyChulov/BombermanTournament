using System.Reflection;
using Core.Engine.Shared.Interfaces;
using Image = Core.Engine.Shared.Objects.GraphicEngine.Draw.Image;

namespace Games.BombermanGame.Shared.DrawDataModel
{
    public class Background :Image
    {
        protected override string LinkedResourceName => "BombermanGame.Background";
        protected override string EmbeddedImageResourceName => 
            "Games.BombermanGame.Shared.EmbeddedResources.background.png";

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