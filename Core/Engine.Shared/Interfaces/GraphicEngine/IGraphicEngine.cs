using System.Drawing;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Core.Engine.Shared.Interfaces.GraphicEngine
{
    public interface IGraphicEngine : IDisposable
    {
        Size CanvasSize { get; }
        ID2D1HwndRenderTarget HwndRenderTarget { get; }
        IDWriteFactory DirectWriteFactory { get; }

        void Start();
        void Stop();
    }
}