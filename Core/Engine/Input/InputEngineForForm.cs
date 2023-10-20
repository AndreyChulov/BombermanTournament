using Core.Engine.Input.Extension;
using Core.Engine.Shared.Interfaces.InputEngine;

namespace Core.Engine.Input;

public class InputEngineForForm : IInputEngine
{
    private readonly Form _form;
    private readonly IInputContainer _inputContainer;
    
    private readonly Size _canvasSize;

    private bool _isStarted = false;
    private Point _mousePosition = Point.Empty;
    
    private PointF _dxCanvasSizeFactor;

    public InputEngineForForm(Form form, float dxCanvasSizeFactor, IInputContainer inputContainer)
    {
        _form = form;
        _dxCanvasSizeFactor = PointF.Empty.FromFloat(dxCanvasSizeFactor);
        _inputContainer = inputContainer;
        _canvasSize = form.Size.Multiplication(dxCanvasSizeFactor);
    }
    
    public void Dispose()
    {
        Stop();
    }

    public void Start()
    {
        if (_isStarted)
        {
            return;
        }
        _form.MouseClick += OnMouseClick;
        _form.MouseMove += OnMouseMove;
        _form.MouseDown +=  OnMouseDown;
        _form.MouseUp += OnMouseUp;
        _form.MouseWheel += OnMouseWheel;
        _form.MouseDoubleClick += OnMouseDoubleClick;
        _form.ResizeEnd += OnResizeEnd;
        _isStarted = true;
    }

    private void OnResizeEnd(object? sender, EventArgs e)
    {
        _dxCanvasSizeFactor = _canvasSize.Divide(_form.Size);
    }

    private void OnMouseDoubleClick(object? sender, EventArgs e)
    {
        _inputContainer.SendMouseDoubleClick(_mousePosition, e);
    }

    private void OnMouseWheel(object? sender, MouseEventArgs e)
    {
        _inputContainer.SendMouseWheel(_mousePosition, e);
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        _inputContainer.SendMouseUp(_mousePosition, e);
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        _inputContainer.SendMouseDown(_mousePosition, e);
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        _mousePosition = e.Location.Multiplication(_dxCanvasSizeFactor);
    }

    private void OnMouseClick(object? sender, EventArgs e)
    {
        _inputContainer.SendMouseClick(_mousePosition, e);
    }

    public void Stop()
    {
        if (!_isStarted)
        {
            return;
        }
        _form.MouseClick -= OnMouseClick;
        _form.MouseMove -= OnMouseMove;
        _form.MouseDown -=  OnMouseDown;
        _form.MouseUp -= OnMouseUp;
        _form.MouseWheel -= OnMouseWheel;
        _form.MouseDoubleClick -= OnMouseDoubleClick;
        _form.ResizeEnd -= OnResizeEnd;
        _isStarted = false;
    }
}