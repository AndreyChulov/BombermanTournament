using System.Net;
using System.Text.Json;
using Core.Network.Client.Client;
using Core.Network.ExternalShared.Contracts.Messages;
using Core.Network.ExternalShared.Interfaces;
using Core.Network.InternalShared;

namespace Core.Network.Client;

public class NetworkClient : INetworkClientObject
{
    private readonly Action _onNetworkClientCreated;
    private readonly Action _onNetworkClientDestroyed;
    private readonly ClientLocatorService _clientLocatorService;
    private ClientService? _clientService;
    private Action<BaseMessage, string>? _onMessageReceived;
    
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

    private void ClientService_OnMessageReceived(BaseMessage? baseMessage, string messageInString)
    {
        if (baseMessage == null)
        {
            return;
        }
        
        switch (baseMessage.Message)
        {
            case PingMessage.MessageString:
                break;
            default:
                if (_onMessageReceived != null)
                {
                    Task.Run(() => _onMessageReceived(baseMessage, messageInString));
                }
                break;
        }
    }

    public void StartClient()
    {
        _clientLocatorService.Start();

        Task.Run(() => _onNetworkClientCreated());
    }

    public void StopClient()
    {
        _clientService?.Stop();
        _clientLocatorService.Stop();
        _clientService?.Dispose();
        _clientLocatorService.Dispose();
        
        _onNetworkClientDestroyed();
    }

    public void SetOnMessageReceivedAction(Action<BaseMessage, string> onMessageReceived)
    {
        _onMessageReceived = onMessageReceived;
        Logger.AddVerboseMessage("OnMessageReceivedAction set.");
    }
    
    public void SendMessage<T>(T messageObject)
        where T:BaseMessage
    {
        _clientService?.SendMessage(messageObject);
    }
}