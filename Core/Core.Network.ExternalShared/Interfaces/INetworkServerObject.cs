namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkServerObject:INetworkObject
{
    void StartServer();
    void StopServer();
}