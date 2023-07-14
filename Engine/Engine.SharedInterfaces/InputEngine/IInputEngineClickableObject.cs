using System.Drawing;

namespace Engine.SharedInterfaces.InputEngine;

public interface IInputEngineClickableObject : IInputEngineObject
{
    void OnClick(Point mousePosition, EventArgs mouseClickEventArgs);
}