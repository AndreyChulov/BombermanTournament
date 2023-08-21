using System.Net;
using Core.Network.Client.Client;
using Core.Network.ExternalShared.Contracts;
using Core.Network.ExternalShared.Interfaces;

namespace Core.Network.Client;

public class NetworkClient : INetworkClientObject
{
    private readonly Action _onNetworkClientCreated;
    private readonly Action _onNetworkClientDestroyed;
    private readonly ClientLocatorService _clientLocatorService;
    private ClientService _clientService;
    
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
        _clientService = new ClientService(
            serverToConnect.Address.ToString(), serverToConnect.Port, 
            ClientService_OnMessageReceived);
        _clientService.Start();
    }

    private void ClientService_OnMessageReceived(BaseMessage? arg1, string arg2)
    {
        throw new NotImplementedException();
    }

    public void StartClient()
    {
        _clientLocatorService.Start();
    }

    public void StopClient()
    {
        _clientService?.Stop();
        _clientLocatorService.Stop();
        _clientService?.Dispose();
        _clientLocatorService.Dispose();
    }
}