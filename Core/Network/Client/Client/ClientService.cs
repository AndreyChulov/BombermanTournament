using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.InternalShared;
using Core.Network.Shared;
using Core.Network.Shared.Contracts.Extensions;
using Core.Network.Shared.Contracts.Messages;

namespace Core.Network.Client.Client;

public class ClientService : BaseThreadService
{
    private readonly string _serverIp;
    private readonly int _serverPort;
    private readonly Action<BaseMessage?, string> _onMessageReceived;
    private readonly Socket _clientSocket;
    private readonly string _pingMessage;
    private readonly Queue<string> _messagesToSend;

    public ClientService(string serverIP, int serverPort, Action<BaseMessage?, string> onMessageReceived) 
        : base(NetworkSettings.ClientQueueCheckTimeout)
    {
        _serverIp = serverIP;
        _serverPort = serverPort;
        _onMessageReceived = onMessageReceived;
        
        _messagesToSend = new Queue<string>();
        _pingMessage = PingMessage.Initialize().Serialize();
        
        _clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
    }

    public void SendMessage<T>(T messageObject)
        where T:BaseMessage
    {
        var jsonMessage = JsonSerializer.Serialize(messageObject);
        _messagesToSend.Enqueue(jsonMessage);
    }
    
    protected override Socket? CreateServiceSocket()
    {
        _clientSocket.Connect(IPAddress.Parse(_serverIp), _serverPort);
            
        return _clientSocket;
    }

    protected override void ServiceWorkerLoop(Socket? serviceSocket)
    {
        if (serviceSocket == null)
        {
            throw new InvalidConstraintException(
                $"${nameof(ClientService)}.{nameof(ServiceWorkerLoop)} can not work with null {nameof(serviceSocket)}");
        }
        
        try
        {
            TcpSocketUtility.SendString(serviceSocket, _pingMessage);
        }
        catch (SocketException) { }
        
        if (!serviceSocket.Connected)
        {
            //Task.Run(() => _onConnectedClientDisconnected((ConnectedClientId)ConnectedClient.ConnectedClientId));
            return;
        }
        
        while (_messagesToSend.Any())
        {
            var messageToSend = _messagesToSend.Dequeue();
            TcpSocketUtility.SendString(serviceSocket, messageToSend);
            Logger.AddVerboseMessage($"Message sent:\n{messageToSend}");
        }

        if (serviceSocket.Available > 0)
        {
            var message = TcpSocketUtility.ReceiveString(serviceSocket, OnReceiveDataSizeCheckFail, OnReceiveDataCheckFail);
            var messageObject = message.Deserialize<BaseMessage>();
            Task.Run(() => _onMessageReceived(messageObject, message));
        }
    }

    private void OnReceiveDataCheckFail()
    {
        throw new NotImplementedException();
    }

    private void OnReceiveDataSizeCheckFail()
    {
        throw new NotImplementedException();
    }

    public override void Stop()
    {
        base.Stop();
        //_clientSocket.Disconnect(false);
    }
}