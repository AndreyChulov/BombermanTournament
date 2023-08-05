using Engine.Input.Extension;
using Engine.SharedInterfaces.InputEngine;

namespace Engine.Input;

public class InputContainer : IInputContainer
{
    private readonly List<IInputEngineObject> _inputEngineObjects;

    public InputContainer()
    {
        _inputEngineObjects = new List<IInputEngineObject>();
    }

    public void AddInputObject(IInputEngineObject? inputObject)
    {
        if (inputObject == null)
        {
            return;
        }
        
        _inputEngineObjects.Add(inputObject);
    }

    public void SendMouseClick(Point mousePosition, EventArgs mouseClickEventArgs)
    {
        var inputEngineMouseInteractableObjectsOnMouse = 
            GetInputEngineMouseInteractableObjectsOnMouse(mousePosition);

        Parallel.ForEach(inputEngineMouseInteractableObjectsOnMouse, 
            (x) => x.OnClick(mousePosition, mouseClickEventArgs));
    }

    private IEnumerable<IInputEngineMouseInteractableObject> GetInputEngineMouseInteractableObjectsOnMouse(Point mousePosition)
    {
        return _inputEngineObjects
            .Where(x=>x.IsMouseOnObject(mousePosition))
            .OfType<IInputEngineMouseInteractableObject>();
    }

    public void SendMouseDown(Point mousePosition, MouseEventArgs mouseEventArgs)
    {
        var inputEngineMouseInteractableObjectsOnMouse = 
            GetInputEngineMouseInteractableObjectsOnMouse(mousePosition);

        Parallel.ForEach(inputEngineMouseInteractableObjectsOnMouse, 
            (x) => x.OnMouseDown(mousePosition, mouseEventArgs));
    }

    public void SendMouseUp(Point mousePosition, MouseEventArgs mouseEventArgs)
    {
        var inputEngineMouseInteractableObjectsOnMouse = 
            GetInputEngineMouseInteractableObjectsOnMouse(mousePosition);

        Parallel.ForEach(inputEngineMouseInteractableObjectsOnMouse, 
            (x) => x.OnMouseUp(mousePosition, mouseEventArgs));
    }

    public void SendMouseDoubleClick(Point mousePosition, EventArgs eventArgs)
    {
        var inputEngineMouseInteractableObjectsOnMouse = 
            GetInputEngineMouseInteractableObjectsOnMouse(mousePosition);

        Parallel.ForEach(inputEngineMouseInteractableObjectsOnMouse, 
            (x) => x.OnMouseDoubleClick(mousePosition, eventArgs));
    }

    public void SendMouseWheel(Point mousePosition, MouseEventArgs mouseEventArgs)
    {
        var inputEngineMouseInteractableObjectsOnMouse = 
            GetInputEngineMouseInteractableObjectsOnMouse(mousePosition);

        Parallel.ForEach(inputEngineMouseInteractableObjectsOnMouse, 
            (x) => x.OnMouseMouseWheel(mousePosition, mouseEventArgs));
    }

    public void Dispose()
    {
        _inputEngineObjects
            .OfType<IDisposable>()
            .ToList()
            .ForEach(x=>x.Dispose());
        _inputEngineObjects.Clear();
    }
}