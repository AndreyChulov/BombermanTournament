namespace Core.Engine.Shared.Interfaces.InputEngine;

public interface IInputEngine : IDisposable
{
    void Start();
    void Stop();
}