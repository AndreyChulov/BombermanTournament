using System.Drawing;

namespace Core.Engine.Shared.Interfaces.InputEngine;

public interface IInputEngineObject
{
    public RectangleF ControlRectangle { get; }
}