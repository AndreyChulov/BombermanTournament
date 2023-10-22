using Core.Network.Shared.Interfaces;

namespace Core.Network.Shared.Contracts;

public class ConnectedClientComparer : IEqualityComparer<IConnectedClient>
{
    public bool Equals(IConnectedClient? x, IConnectedClient? y)
    {
        if (x == null && y == null)
        {
            return true;
        }
        
        if (x == null || y == null)
        {
            return false;
        }
        
        return x.Equals(y);
    }
    
    public int GetHashCode(IConnectedClient connectedClient)
    {
        return connectedClient.GetHashCode();
    }
}