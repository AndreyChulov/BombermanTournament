using Engine.SharedInterfaces.GraphicEngine;
using Engine.SharedInterfaces.GraphicEngine.Draw;
using Engine.SharedInterfaces.GraphicEngine.RamResources;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using FactoryType = Vortice.Direct2D1.FactoryType;
using PixelFormat = Vortice.DCommon.PixelFormat;
using Timer = System.Threading.Timer;

namespace Engine.Graphic
{
    public class GraphicEngineForForm : IGraphicEngine
    {
        private readonly IntPtr _formHwnd;
        private readonly TimeSpan _renderTimerTimeout;
        
        private Size _formSize;

        private Timer _renderTimer;

        private IResourcesContainer _resourcesContainer;
        private IDrawContainer _drawContainer;

        public Size CanvasSize { get; private set; }

        public ID2D1HwndRenderTarget HwndRenderTarget { get; private set; }
        public IDWriteFactory DirectWriteFactory { get; private set; }

        public GraphicEngineForForm(
            IntPtr hwnd, Size formSize, float dxCanvasSizeFactor,
            IResourcesContainer resourcesContainer, 
            IDrawContainer drawContainer)
        {
            _formHwnd = hwnd;
            _formSize = formSize;
            _resourcesContainer = resourcesContainer;
            _drawContainer = drawContainer;
            
            CanvasSize = new Size(
                (int)(formSize.Width * dxCanvasSizeFactor), 
                (int)(formSize.Height * dxCanvasSizeFactor)
                );
            _renderTimerTimeout = TimeSpan.FromMilliseconds(10);

            _renderTimer = new Timer(OnRender_timerCallback);
            _renderTimer.Change(Timeout.InfiniteTimeSpan, _renderTimerTimeout);

            HwndRenderTarget = InitialiseHwndRenderTarget();
            DirectWriteFactory = DWrite.DWriteCreateFactory<IDWriteFactory>(Vortice.DirectWrite.FactoryType.Shared);
        }
        
        
        
        
        
        
        
        
        
        
        

        private void OnRender_timerCallback(object? state)
        {
            HwndRenderTarget.BeginDraw();
            HwndRenderTarget.Clear(new Color4(Color3.Black, 1f));
            _drawContainer.DrawAll(HwndRenderTarget);
            HwndRenderTarget.EndDraw();
        }

        private ID2D1HwndRenderTarget InitialiseHwndRenderTarget()
        {
            using var d2d1Factory = D2D1.D2D1CreateFactory<ID2D1Factory>(
                FactoryType.MultiThreaded, DebugLevel.None);
            var renderTargetProperties = new RenderTargetProperties(PixelFormat.Premultiplied) { };
            var hwndRenderTargetProperties = new HwndRenderTargetProperties()
            {
                Hwnd = _formHwnd, 
                PixelSize = CanvasSize,//new Size(100,100),
                PresentOptions = PresentOptions.RetainContents
            };
            return d2d1Factory.CreateHwndRenderTarget(renderTargetProperties, hwndRenderTargetProperties);
        }

        public void Start()
        {
            _renderTimer.Change(TimeSpan.Zero, _renderTimerTimeout);
        }

        public void Stop()
        {
            _renderTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        public void Dispose()
        {
            HwndRenderTarget.Dispose();
            _renderTimer.Dispose();
            _resourcesContainer.Dispose();
            _drawContainer.Dispose();
        }
    }
}