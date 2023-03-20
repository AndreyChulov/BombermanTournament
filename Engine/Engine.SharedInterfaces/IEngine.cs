using System.Drawing;
using Engine.SharedInterfaces.GraphicEngine.Draw;

namespace Engine.SharedInterfaces
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