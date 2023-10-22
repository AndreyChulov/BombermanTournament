using System;
using System.Threading.Tasks;

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

    /// <summary>
    /// Function tracks update monitored value
    /// <para></para>
    /// 
    /// </summary>
    /// <value>By default <paramref name="isUpdatedCheck"/> is null</value>
    /// <param name="updateAction">Action to update monitored value</param>
    /// <param name="isUpdatedCheck">Action to validate, is monitored value was updated</param>
    /// <remarks>
    /// If <paramref name="isUpdatedCheck"/> is null,
    /// <paramref name="isUpdatedCheck"/> function always returns true
    /// </remarks>
    public void Update(Action<T> updateAction, Func<T, bool>? isUpdatedCheck = null)
    {
        updateAction.Invoke(_variable);

        if (isUpdatedCheck?.Invoke(_variable) ?? true)
        {
            Task.Run(OnChanged.Invoke);
        }
    }

    public static implicit operator MonitoredVariable<T>(T variable) => new(variable);
    public static implicit operator T(MonitoredVariable<T> monitoredVariable) => monitoredVariable._variable;
}