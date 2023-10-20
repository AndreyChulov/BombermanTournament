using Core.Network.Shared.Contracts;

namespace Core.Network.Shared.Interfaces;

public interface INetworkServerObject:INetworkObject
{
    public IConnectedClient[] ConnectedClients { get; }
    public string ServerIP { get; }
    public int ServerPort { get; }
    public int ConnectedClientsCount { get; }
    public bool IsLocatorServiceStarted { get; }
    
    void CreateServer();
    void DestroyServer();
    void StartLocatorService();
    void StopLocatorService();
    void SetOnClientConnectedAction(Action onClientConnected);
    void SetOnClientUpdatedAction(Action<ConnectedClientId> onClientUpdated);
    void SetOnClientDisconnectedAction(Action<ConnectedClientId> onClientDisconnected);

}