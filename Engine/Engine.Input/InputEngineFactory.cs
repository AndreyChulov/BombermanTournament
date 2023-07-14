using Engine.SharedInterfaces.InputEngine;

namespace Engine.Input;

public static class InputEngineFactory
{
    public static IInputEngine CreateInputEngine(Form form, float dxCanvasSizeFactor, IInputContainer inputContainer)
    {
        return new InputEngineForForm(form, dxCanvasSizeFactor, inputContainer);
    }
}