using System;
using Vortice.DCommon;
using Vortice.Direct2D1;
using Vortice.DXGI;

namespace TestDX12_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //DXGI.
            //new ID2D1Device()
            //Vortice.DXGI.
            //var device = D2D1.D2D1CreateDevice()
            //Vortice.DXCore
            //D2D1.cre
            var d2d1Factory = D2D1.D2D1CreateFactory<ID2D1Factory>(FactoryType.SingleThreaded, DebugLevel.None);
            var renderTargetProperties = new RenderTargetProperties(PixelFormat.Premultiplied);
            var hwndRenderTargetProperties = new HwndRenderTargetProperties();
            var hwndRenderTarget = d2d1Factory.CreateHwndRenderTarget(renderTargetProperties, hwndRenderTargetProperties);
            //hwndRenderTarget.
            //d2d1Factory.
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
