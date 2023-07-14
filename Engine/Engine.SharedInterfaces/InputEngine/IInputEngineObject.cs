using System.Drawing;

namespace Engine.SharedInterfaces.InputEngine;

public interface IInputEngineObject
{
    public Rectangle ControlRectangle { get; }
}