using System.Drawing;
using Core.Engine.Shared.Interfaces.GraphicEngine.Draw;

namespace Core.Engine.Shared.Interfaces
{
    public interface IEngine : IDisposable
    {
        Size GetCanvasSize();
        void LoadResources();
        void LoadDrawObjects(IDraw[] drawObjects);
        void LoadDrawObject(IDraw drawObject);
        void Start();
        void Stop();
    }
}