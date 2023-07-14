using System.Drawing;

namespace Engine.SharedInterfaces.InputEngine;

public interface IInputContainer : IDisposable
{
    void AddInputObject(IInputEngineObject? inputObject);
    void SendMouseClick(Point mousePosition, EventArgs mouseClickEventArgs);
}   