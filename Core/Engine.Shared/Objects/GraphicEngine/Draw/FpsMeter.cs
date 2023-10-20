using System.Drawing;
using Core.Engine.Shared.Interfaces;
using Vortice.Direct2D1;
using Timer = System.Threading.Timer;

namespace Core.Engine.Shared.Objects.GraphicEngine.Draw
{
    public class FpsMeter : TextWithShadow, IDisposable
    {
        private static string ConstructString(int? fps) => fps.HasValue ? $"FPS:{fps.Value}" : "FPS:undefined";

        private int _currentFps = 0;
        private Timer _timer;
        
        public static FpsMeter CreateInPercents(IEngine engine, RectangleF drawRectangle)
        {
            var canvasSize = engine.GetCanvasSize();
            var canvasWidth = canvasSize.Width;
            var canvasHeight = canvasSize.Height;

            return new FpsMeter(
                new Rectangle(
                    (int)(drawRectangle.X * canvasWidth),
                    (int)(drawRectangle.Y * canvasHeight),
                    (int)(drawRectangle.Width * canvasWidth),
                    (int)(drawRectangle.Height * canvasHeight)
                ), 
                canvasHeight * 0.03f 
            );
        }

        protected FpsMeter(Rectangle drawRectangle, float fontSize) 
            : base(ConstructString(null), drawRectangle, fontSize)
        {
            _timer = new Timer(Timer_Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        private void Timer_Callback(object? state)
        {
            base.TextToDraw = ConstructString(_currentFps);
            _currentFps = 0;
        }

        public override void Draw(ID2D1HwndRenderTarget renderTarget)
        {
            base.Draw(renderTarget);
            
            _currentFps++;
        }
        
        public void Dispose()
        {
            _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            _timer.Dispose();
        }
    }
}