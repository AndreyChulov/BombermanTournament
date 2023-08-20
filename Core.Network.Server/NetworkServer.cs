using Core.Network.ExternalShared;
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

    public NetworkServer(Action onServerCreated, Action onServerDestroyed)
    {
        _onServerCreated = onServerCreated;
        _onServerDestroyed = onServerDestroyed;
        _serverService = new ServerService(TimeSpan.FromSeconds(1));
        _serverLocatorService = new ServerLocatorService(_serverService.TcpPort);
    }

    public void CreateServer()
    {
        Logger.Initialize(NetworkSettings.ServerLogsFile);
        //_serverService.Start();
        StartLocatorService();
        _onServerCreated();
    }

    public void DestroyServer()
    {
        StopLocatorService();
        //_serverService.Stop();
        Logger.FreeUpResources();
        _serverLocatorService.Dispose();
        _serverService.Dispose();
        _onServerDestroyed();
    }

    public void StartLocatorService()
    {
        _serverLocatorService.Start();
    }

    public void StopLocatorService()
    {
        _serverLocatorService.Stop();
    }
}