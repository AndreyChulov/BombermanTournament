using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using Core.Network.Server;

namespace Core.Network;

public static class NetworkFactory
{
    private static INetworkObject CreateNetworkObject(NetworkObjectType networkObjectType)
    {
        switch (networkObjectType)
        {
            case NetworkObjectType.Server:
                return new NetworkServer();
            default:
                throw new NotImplementedException();
        }
    }
    
    public static T CreateNetworkObject<T>(NetworkObjectType networkObjectType) where T:INetworkObject
    {
        return (T)CreateNetworkObject(networkObjectType);
    }
}