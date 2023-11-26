using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.InternalShared;
using Core.Network.Shared;
using Core.Network.Shared.Contracts;
using Core.Network.Shared.Contracts.Extensions;
using Core.Network.Shared.Contracts.Messages;

namespace Core.Network.Server.Server;

public class ConnectedClientService : BaseThreadService
{
    private readonly Socket _clientSocket;
    private readonly Action<ConnectedClientId> _onConnectedClientUpdated;
    private readonly Action<ConnectedClientId> _onConnectedClientDisconnected;
    private readonly Queue<string> _messagesToSend;
    private readonly string _pingMessage;

    public ConnectedClient ConnectedClient { get; private set; }

    public ConnectedClientService(Socket clientSocket, 
        Action<ConnectedClientId> onConnectedClientUpdated, 
        Action<ConnectedClientId> onConnectedClientDisconnected) 
        : base(NetworkSettings.ClientQueueCheckTimeout)
    {
        _clientSocket = clientSocket;
        _onConnectedClientUpdated = onConnectedClientUpdated;
        _onConnectedClientDisconnected = onConnectedClientDisconnected;

        _messagesToSend = new Queue<string>();

        var ipEndpoint = (IPEndPoint?)clientSocket.RemoteEndPoint;
        var connectedClientId = new ConnectedClientId(
            ipEndpoint?.Address.MapToIPv4().ToString() ?? "unknown",
            ipEndpoint?.Port ?? -1
        );
        ConnectedClient = new ConnectedClient(connectedClientId, SendMessage);
        _pingMessage = PingMessage.Initialize().Serialize();
    }

    private void SendMessage<T>(T messageObject)
        where T:BaseMessage
    {
        var jsonMessage = JsonSerializer.Serialize(messageObject, messageObject.GetType());
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

        try
        {
            TcpSocketUtility.SendString(serviceSocket, _pingMessage);
        }
        catch (SocketException) { }
        
        if (!serviceSocket.Connected)
        {
            Task.Run(() => _onConnectedClientDisconnected((ConnectedClientId)ConnectedClient.ConnectedClientId));
            return;
        }

        while (_messagesToSend.Any())
        {
            var messageToSend = _messagesToSend.Dequeue();
            TcpSocketUtility.SendString(serviceSocket, messageToSend);
        }

        while (serviceSocket.Available > 0)
        {
            var message = TcpSocketUtility.ReceiveString(
                serviceSocket, OnReceiveDataSizeCheckFail, OnReceiveDataCheckFail);
            //Console.WriteLine(
            //    $"{DateTime.Now.ToLongTimeString()} => Message recieved [{message}]");
            var messageObject = message.Deserialize<BaseMessage>();
            Task.Run(() => OnMessageReceived(messageObject, message));
        }
    }

    private void OnMessageReceived(BaseMessage? baseMessage, string serializedMessage)
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
                ConnectedClient.RiseMessageReceived(baseMessage, serializedMessage);
                break;
        }
    }

    private void OnReceiveDataCheckFail()
    {
    }

    private void OnReceiveDataSizeCheckFail()
    {
    }
}