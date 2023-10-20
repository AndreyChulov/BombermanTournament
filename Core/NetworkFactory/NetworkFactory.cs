using Core.Network.Client;
using Core.Network.Server;
using Core.Network.Shared.Enums;
using Core.Network.Shared.Interfaces;

namespace Core.Network;

public static class NetworkFactory
{
    private static INetworkObject CreateNetworkObject(
        NetworkObjectType networkObjectType, Action onNetworkObjectCreated, Action onNetworkObjectDestroyed)
    {
        switch (networkObjectType)
        {
            case NetworkObjectType.Server:
                return new NetworkServer(onNetworkObjectCreated, onNetworkObjectDestroyed);
            default:
                return new NetworkClient(onNetworkObjectCreated, onNetworkObjectDestroyed);
        }
    }
    
    public static T CreateNetworkObject<T>(
        NetworkObjectType networkObjectType, Action onNetworkObjectCreated, Action onNetworkObjectDestroyed) 
        where T:INetworkObject
    {
        return (T)CreateNetworkObject(networkObjectType, onNetworkObjectCreated, onNetworkObjectDestroyed);
    }
}