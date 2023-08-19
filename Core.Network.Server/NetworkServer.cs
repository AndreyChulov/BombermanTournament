using Core.Network.ExternalShared.Interfaces;

namespace Core.Network.Server;

public class NetworkServer : INetworkObject
{
    private readonly Action _onServerCreated;
    private readonly Action _onServerDestroyed;

    public NetworkServer(Action onServerCreated, Action onServerDestroyed)
    {
        _onServerCreated = onServerCreated;
        _onServerDestroyed = onServerDestroyed;
        throw new NotImplementedException();
    }
}