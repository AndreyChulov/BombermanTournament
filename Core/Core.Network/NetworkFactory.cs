using Core.Network.Client;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using Core.Network.Server;

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