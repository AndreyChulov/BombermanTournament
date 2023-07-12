using System.ComponentModel;
using Engine.Shared.GraphicEngine.Draw;
using TestInputDevelopment.Draw;

namespace TestInputDevelopment;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void OnHandleCreated(object? sender, EventArgs e)
    {
        _engine = Engine.EngineFactory.CreateEngine(this, 2);
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        _engine.Stop();
    }

    private void OnLoad(object? sender, EventArgs e)
    {
        _engine.LoadDrawObject(Background.CreateInstance(_engine));
        _engine.LoadDrawObject(FpsMeter.CreateInPercents(_engine, new RectangleF(0.8f, 0.05f, 0.20f, 0.05f)));
        _engine.LoadDrawObject(Input.Button.CreateInPercents(_engine, new RectangleF(0.2f, 0.2f, 0.3f, 0.3f)));
        _engine.LoadResources();
        _engine.Start();
    }
}