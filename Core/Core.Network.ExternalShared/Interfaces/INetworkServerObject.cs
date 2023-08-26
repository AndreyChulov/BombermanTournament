using Core.Network.ExternalShared.Contracts;

namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkServerObject:INetworkObject
{
    public IConnectedClient[] ConnectedClients { get; }
    public string ServerIP { get; }
    public int ServerPort { get; }
    public int ConnectedClientsCount { get; }
    
    void CreateServer();
    void DestroyServer();
    void StartLocatorService();
    void StopLocatorService();
    void SetOnClientConnectedAction(Action onClientConnected);
    void SetOnClientUpdatedAction(Action<ConnectedClientId> onClientUpdated);
}