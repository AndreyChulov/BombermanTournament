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
        var filteredObjects = _inputEngineObjects
            .Where(x=>x.IsMouseOnObject(mousePosition))
            .OfType<IInputEngineClickableObject>();

        Parallel.ForEach(filteredObjects, (x) => x.OnClick(mousePosition, mouseClickEventArgs));
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