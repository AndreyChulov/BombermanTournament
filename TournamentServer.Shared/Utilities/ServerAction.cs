namespace TournamentServer.Shared.Utilities;

public class ServerAction
{
    private List<Action> _actions = new List<Action>();

    public void Invoke()
    {
        foreach (var action in _actions)
        {
            action.Invoke();
        }
    }

    public void AddAction(Action action)
    {
        _actions.Add(action);
    }
    
    public void RemoveAction(Action action)
    {
        _actions.Remove(action);
    }
}