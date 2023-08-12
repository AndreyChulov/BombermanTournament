using System;

namespace TournamentServer.Server.Utilities;

public class ServerAction
{
    private Action? _action;

    public void Invoke()
    {
        _action?.Invoke();
    }
    
    public static implicit operator ServerAction(Action action) => new() { _action = action };
    public static implicit operator Action?(ServerAction serverAction) => serverAction._action;
}