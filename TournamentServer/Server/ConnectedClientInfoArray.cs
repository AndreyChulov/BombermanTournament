using System;
using System.Linq;
using System.Threading.Tasks;
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

    public void OnClientUpdated(ConnectedClientId id)
    {
        Task.Run(() => this[id].OnClientUpdated());
    }

    private ConnectedClientInfoArray(IConnectedClientInfo[] connectedClientInfos)
    {
        _connectedClientInfoArray = connectedClientInfos;
    }

    public void ForEach(Action<IConnectedClientInfo> predicate)
    {
        foreach (var connectedClientInfo in _connectedClientInfoArray)
        {
            predicate(connectedClientInfo);
        }
    }

    public bool All(Func<IConnectedClientInfo, bool> predicate) => _connectedClientInfoArray.All(predicate);

    public IConnectedClientInfo this[int index] => 
        index >= _connectedClientInfoArray.Length ? 
            new ConnectedClientInfoStub() :
            _connectedClientInfoArray[index];
    private IConnectedClientInfo this[ConnectedClientId id] => 
        _connectedClientInfoArray.FirstOrDefault(x => x.ConnectedClientId.Equals(id)) ?? 
            (IConnectedClientInfo)new ConnectedClientInfoStub();
    
    public static implicit operator ConnectedClientInfoArray(IConnectedClientInfo[] connectedClientInfos) =>
        new(connectedClientInfos);

    public static implicit operator IConnectedClientInfo[](ConnectedClientInfoArray connectedClientInfoArray) =>
        connectedClientInfoArray._connectedClientInfoArray;
}