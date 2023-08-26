using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.InternalShared;

namespace Core.Network.Server.Server;

public class ConnectedClientService : BaseThreadService
{
    private readonly Socket _clientSocket;
    private readonly Action _onConnectedClientUpdated;
    private readonly Queue<string> _messagesToSend;

    public ConnectedClient ConnectedClient { get; private set; }

    public ConnectedClientService(Socket clientSocket, Action onConnectedClientUpdated) 
        : base(NetworkSettings.ClientQueueCheckTimeout)
    {
        _clientSocket = clientSocket;
        _onConnectedClientUpdated = onConnectedClientUpdated;

        _messagesToSend = new Queue<string>();

        var ipEndpoint = (IPEndPoint?)clientSocket.RemoteEndPoint;
        var connectedClientId = new ConnectedClientId(
            ipEndpoint?.Address.MapToIPv4().ToString() ?? "unknown",
            ipEndpoint?.Port ?? -1
        );
        ConnectedClient = new ConnectedClient(connectedClientId);
    }

    public void SendMessage<T>(T messageObject)
        where T:BaseMessage
    {
        var jsonMessage = JsonSerializer.Serialize<T>(messageObject);
        _messagesToSend.Enqueue(jsonMessage);
    }
    
    protected override Socket? CreateServiceSocket()
    {
        return _clientSocket;
    }

    protected override void ServiceWorkerLoop(Socket? serviceSocket)
    {
        if (serviceSocket == null)
        {
            throw new InvalidConstraintException(
                $"${nameof(ConnectedClientService)}.{nameof(ServiceWorkerLoop)} can not work with null {nameof(serviceSocket)}");
        }

        while (_messagesToSend.Any())
        {
            var messageToSend = _messagesToSend.Dequeue();
            TcpSocketUtility.SendString(serviceSocket, messageToSend);
        }
    }
}