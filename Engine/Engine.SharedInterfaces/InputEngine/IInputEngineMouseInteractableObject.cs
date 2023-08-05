namespace Engine.SharedInterfaces.InputEngine;

public interface IInputEngineMouseInteractableObject : IInputEngineObject
{
    void OnClick(Point mousePosition, EventArgs mouseClickEventArgs);
    void OnMouseDown(Point mousePosition, MouseEventArgs mouseClickEventArgs);
    void OnMouseUp(Point mousePosition, MouseEventArgs mouseClickEventArgs);
    void OnMouseDoubleClick(Point mousePosition, EventArgs mouseClickEventArgs);
    void OnMouseMouseWheel(Point mousePosition, MouseEventArgs mouseClickEventArgs);
}