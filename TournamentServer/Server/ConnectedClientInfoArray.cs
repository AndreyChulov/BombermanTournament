using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Network.Shared.Contracts;
using Core.Network.Shared.Interfaces;

namespace TournamentServer.Server;

public class ConnectedClientInfoArray : IEquatable<ConnectedClientInfoArray>
{
    private IConnectedClientInfo[] _connectedClientInfoArray;

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

    public void Update(IConnectedClient[] connectedClients)
    {
        var comparer = new ConnectedClientComparer();

        List<ConnectedClientInfo> connectedClientInfos = new List<ConnectedClientInfo>();

        foreach (var connectedClient in connectedClients)
        {
            var connectedClientInfo = _connectedClientInfoArray
                .FirstOrDefault(x => (x as IConnectedClient)?.Equals(connectedClient) ?? false, null);
            connectedClientInfos.Add(
                connectedClientInfo as ConnectedClientInfo ?? new ConnectedClientInfo(connectedClient));
        }

        _connectedClientInfoArray = connectedClientInfos.ToArray();
    }

    public void ForEach(Action<IConnectedClientInfo> predicate)
    {
        foreach (var connectedClientInfo in _connectedClientInfoArray)
        {
            predicate(connectedClientInfo);
        }
    }

    public IConnectedClientInfo FirstOrDefault(
        Func<IConnectedClientInfo, bool> predicate, IConnectedClientInfo @default) =>
            _connectedClientInfoArray.FirstOrDefault(predicate, @default);
    
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