using Core.Engine.Shared.Interfaces.InputEngine;

namespace Core.Engine.Input.Extension;

public static class IInputEngineObjectExtension
{
    public static bool IsMouseOnObject(this IInputEngineObject inputEngineObject, Point mouseLocation)
    {
        var objectLocation = inputEngineObject.ControlRectangle;
        
        return objectLocation.Left < mouseLocation.X && objectLocation.Right > mouseLocation.X &&
               objectLocation.Top < mouseLocation.Y && objectLocation.Bottom > mouseLocation.Y;
    }
}