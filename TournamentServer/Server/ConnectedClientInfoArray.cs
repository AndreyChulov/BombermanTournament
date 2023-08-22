using System;

namespace TournamentServer.Server;

public class ConnectedClientInfoArray : IEquatable<ConnectedClientInfoArray>
{
    private readonly IConnectedClientInfo[] _connectedClientInfoArray;

    public bool Equals(ConnectedClientInfoArray? other)
    {
        if (other == null)
        {
            return false;
        }

        if (_connectedClientInfoArray.Length != other._connectedClientInfoArray.Length)
        {
            return false;
        }

        return true;
    }

    private ConnectedClientInfoArray(IConnectedClientInfo[] connectedClientInfos)
    {
        _connectedClientInfoArray = connectedClientInfos;
    }

    public IConnectedClientInfo this[int index] => _connectedClientInfoArray[index];
    
    public static implicit operator ConnectedClientInfoArray(IConnectedClientInfo[] connectedClientInfos) =>
        new(connectedClientInfos);

    public static implicit operator IConnectedClientInfo[](ConnectedClientInfoArray connectedClientInfoArray) =>
        connectedClientInfoArray._connectedClientInfoArray;

}