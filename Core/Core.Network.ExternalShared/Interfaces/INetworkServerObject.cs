namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkServerObject:INetworkObject
{
    public string[] ConnectedClients { get; }
    public string ServerIP { get; }
    public int ServerPort { get; }
    public int ConnectedClientsCount { get; }
    
    void CreateServer();
    void DestroyServer();
    void StartLocatorService();
    void StopLocatorService();
    void SetOnClientConnectedAction(Action onClientConnected);
}