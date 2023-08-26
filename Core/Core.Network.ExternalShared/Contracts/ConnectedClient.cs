using Core.Network.ExternalShared.Interfaces;

namespace Core.Network.ExternalShared.Contracts;

public class ConnectedClient : IConnectedClient
{
    public IConnectedClientId ConnectedClientId { get; }

    public ConnectedClient(IConnectedClientId connectedClientId)
    {
        ConnectedClientId = connectedClientId;
    }

    public bool Equals(IConnectedClient? other)
    {
        return ConnectedClientId.Equals(other?.ConnectedClientId);
    }

}