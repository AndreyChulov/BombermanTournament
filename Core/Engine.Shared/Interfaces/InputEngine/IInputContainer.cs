using System.Drawing;

namespace Core.Engine.Shared.Interfaces.InputEngine;

public interface IInputContainer : IDisposable
{
    void AddInputObject(IInputEngineObject? inputObject);
    void SendMouseClick(Point mousePosition, EventArgs mouseClickEventArgs);
    void SendMouseDown(Point mousePosition, MouseEventArgs mouseEventArgs);
    void SendMouseUp(Point mousePosition, MouseEventArgs mouseEventArgs);
    void SendMouseDoubleClick(Point mousePosition, EventArgs eventArgs);
    void SendMouseWheel(Point mousePosition, MouseEventArgs mouseEventArgs);
}   