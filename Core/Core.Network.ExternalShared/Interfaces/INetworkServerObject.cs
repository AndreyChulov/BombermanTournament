namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkServerObject:INetworkObject
{
    void CreateServer();
    void DestroyServer();
    void StartLocatorService();
    void StopLocatorService();
}