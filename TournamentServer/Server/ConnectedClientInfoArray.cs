using System;
using System.Linq;
using Core.Network.ExternalShared.Contracts;

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

    public IConnectedClientInfo this[int index] => 
        index >= _connectedClientInfoArray.Length ? 
            new ConnectedClientInfoStub() :
            _connectedClientInfoArray[index];
    public IConnectedClientInfo this[ConnectedClientId id] => 
        _connectedClientInfoArray.FirstOrDefault(x => x.ConnectedClientId.Equals(id)) ?? 
            (IConnectedClientInfo)new ConnectedClientInfoStub();
    
    public static implicit operator ConnectedClientInfoArray(IConnectedClientInfo[] connectedClientInfos) =>
        new(connectedClientInfos);

    public static implicit operator IConnectedClientInfo[](ConnectedClientInfoArray connectedClientInfoArray) =>
        connectedClientInfoArray._connectedClientInfoArray;

}