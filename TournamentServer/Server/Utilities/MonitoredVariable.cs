using System;

namespace TournamentServer.Server.Utilities;

public class MonitoredVariable<T> where T:IEquatable<T>
{
    private T _variable;

    public ServerAction OnChanged { get; set; } = new();

    private MonitoredVariable(T variable)
    {
        _variable = variable;
    }

    public void SetVariable(T value)
    {
        if (_variable.Equals(value))
        {
            return;
        }
        
        _variable = value;
        OnChanged.Invoke();
    }

    public static implicit operator MonitoredVariable<T>(T variable) => new(variable);
    public static implicit operator T(MonitoredVariable<T> monitoredVariable) => monitoredVariable._variable;
}