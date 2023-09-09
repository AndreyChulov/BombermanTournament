using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.ExternalShared.Interfaces;
using Core.Network.InternalShared;
using Core.Network.Server.Server;

namespace Core.Network.Server;

public class NetworkServer : INetworkServerObject
{
    private readonly Action _onServerCreated;
    private readonly Action _onServerDestroyed;
    
    private readonly ServerService _serverService;
    private readonly ServerLocatorService _serverLocatorService;
    
    private Action? _onClientConnected;
    private Action<ConnectedClientId>? _onClientUpdated;
    private Action<ConnectedClientId>? _onClientDisconnected;

    public IConnectedClient[] ConnectedClients => _serverService
        .ConnectedClientServices
        .Select(x => (IConnectedClient)x.ConnectedClient)
        .ToArray();

    public string ServerIP => _serverService.ServerIp;
    public int ServerPort => _serverService.ServerPort;
    public int ConnectedClientsCount => _serverService
        .ConnectedClientServices
        .Count();

    public bool IsLocatorServiceStarted => _serverLocatorService.IsServiceStarted;
    

    public NetworkServer(Action onServerCreated, Action onServerDestroyed)
    {
        _onServerCreated = onServerCreated;
        _onServerDestroyed = onServerDestroyed;
        _onClientConnected = null;
        _onClientDisconnected = null;
        _serverService = new ServerService(
            ServerService_OnClientConnected, ServerService_OnClientUpdated, ServerService_OnClientDisconnected);
        _serverLocatorService = new ServerLocatorService(_serverService.ServerPort);
    }

    private void ServerService_OnClientDisconnected(ConnectedClientId connectedClientId)
    {
        if (_onClientDisconnected == null)
        {
            return;
        }
        
        Task.Run(() => _onClientDisconnected(connectedClientId));
    }

    private void ServerService_OnClientUpdated(ConnectedClientId connectedClientId)
    {
        if (_onClientUpdated == null)
        {
            return;
        }

        Task.Run(() => _onClientUpdated(connectedClientId));
    }

    private void ServerService_OnClientConnected()
    {
        if (_onClientConnected == null)
        {
            return;
        }

        Task.Run(() => _onClientConnected());
    }

    public void CreateServer()
    {
        Logger.Initialize(NetworkSettings.ServerLogsFile);
        _serverService.Start();
        StartLocatorService();
        
        Task.Run(() => _onServerCreated());
    }

    public void DestroyServer()
    {
        StopLocatorService();
        _serverService.Stop();
        _serverLocatorService.Dispose();
        _serverService.Dispose();
        Logger.FreeUpResources();
        
        Task.Run(() => _onServerDestroyed());
    }

    public void StartLocatorService()
    {
        _serverLocatorService.Start();
    }

    public void StopLocatorService()
    {
        _serverLocatorService.Stop();
    }

    public void SetOnClientConnectedAction(Action onClientConnected)
    {
        _onClientConnected = onClientConnected;
    }
    
    public void SetOnClientDisconnectedAction(Action<ConnectedClientId> onClientDisconnected)
    {
        _onClientDisconnected = onClientDisconnected;
    }
    
    public void SetOnClientUpdatedAction(Action<ConnectedClientId> onClientUpdated)
    {
        _onClientUpdated = onClientUpdated;
    }
}