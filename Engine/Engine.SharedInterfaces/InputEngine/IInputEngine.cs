namespace Engine.SharedInterfaces.InputEngine;

public interface IInputEngine : IDisposable
{
    void Start();
    void Stop();
}