using System;
using System.Drawing;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using Engine;
using Engine.Shared.GraphicEngine.Draw;
using Engine.SharedInterfaces;
using Vortice.Direct2D1;
using Image = Engine.Shared.GraphicEngine.Draw.Image;

namespace TestDX12_3
{
    public partial class Form1 : Form
    {
        private IEngine? _engine = null;
        
        public Form1()
        {
            InitializeComponent();
        }

        //ID2D1RenderTarget _hwndRenderTarget;

        private void Form1_Load(object sender, EventArgs e)
        {
            _engine = EngineFactory.CreateEngine(this, 2f);
            _engine.LoadDrawObject(
                Image.CreateInPercents(
                    _engine, new RectangleF(0f, 0f, 1f, 1f)
                )
            );
            _engine.LoadDrawObject(
                Line.CreateInPercents(
                    _engine, 
                    new Vector2(0,0), 
                    new Vector2(1,1)
                )
            );
            _engine.LoadDrawObject(
                Engine.Shared.GraphicEngine.Draw.Text.CreateInPercents(
                    _engine, "Hello world",
                    new RectangleF(0.1f, 0.1f, 0.8f, 0.8f)
                )
            );
            _engine.LoadDrawObject(
                FpsMeter.CreateInPercents(
                    _engine, new RectangleF(0.93f, 0.02f, 0.07f, 0.05f)
                )
            );
            _engine.LoadDrawObject(
                RoundedRectangleFilled.CreateInPercents(_engine,
                    new RoundedRectangle(
                        new RectangleF(0.1f,0.1f,0.8f,0.8f),
                        0.03f, 0.03f))
                );
            _engine.LoadResources();
            _engine.Start();
            //var dcRenderTarget = d2d1Factory.CreateDCRenderTarget(renderTargetProperties);
            //d2d1Factory.

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            /*_hwndRenderTarget.BeginDraw();
            var brush = _hwndRenderTarget.CreateSolidColorBrush(new Color4(Color3.Yellow, 0.5f));
            _hwndRenderTarget.DrawLine(new Vector2(0,0), new Vector2(100,100), brush);
            _hwndRenderTarget.EndDraw();*/
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _engine?.Stop();
            _engine?.Dispose();
        }
    }
    
}

