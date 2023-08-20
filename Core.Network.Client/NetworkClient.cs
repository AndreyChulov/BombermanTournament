using System.Net;
using Core.Network.Client.Client;
using Core.Network.ExternalShared.Interfaces;

namespace Core.Network.Client;

public class NetworkClient : INetworkClientObject
{
    private readonly Action _onNetworkClientCreated;
    private readonly Action _onNetworkClientDestroyed;
    private readonly ClientLocatorService _clientLocatorService;
    
    public NetworkClient(Action onNetworkClientCreated, Action onNetworkClientDestroyed)
    {
        _onNetworkClientCreated = onNetworkClientCreated;
        _onNetworkClientDestroyed = onNetworkClientDestroyed;
        _clientLocatorService = new ClientLocatorService(ClientLocatorService_OnServerAdded);
    }

    private void ClientLocatorService_OnServerAdded(List<IPEndPoint> servers)
    {
        var serverToConnect = servers.Last();
        _clientLocatorService.Stop();
        throw new NotImplementedException();
    }

    public void StartClient()
    {
        _clientLocatorService.Start();
    }

    public void StopClient()
    {
        _clientLocatorService.Stop();
        _clientLocatorService.Dispose();
    }
}