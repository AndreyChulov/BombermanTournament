using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.InternalShared;

namespace Core.Network.Client.Client;

public class ClientService : BaseThreadService
{
    private readonly string _serverIp;
    private readonly int _serverPort;
    private readonly Action<BaseMessage?, string> _onMessageReceived;

    public ClientService(string serverIP, int serverPort, Action<BaseMessage?, string> onMessageReceived) : base(NetworkSettings.ClientQueueCheckTimeout)
    {
        _serverIp = serverIP;
        _serverPort = serverPort;
        _onMessageReceived = onMessageReceived;
    }

    protected override Socket? CreateServiceSocket()
    {
        var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        //socket.EnableBroadcast = false;
        socket.Connect(IPAddress.Parse(_serverIp), _serverPort);
            
        return socket;
    }

    protected override void ServiceWorkerLoop(Socket? serviceSocket)
    {
        if (serviceSocket == null)
        {
            throw new InvalidConstraintException(
                $"${nameof(ClientService)}.{nameof(ServiceWorkerLoop)} can not work with null {nameof(serviceSocket)}");
        }

        if (serviceSocket.Available > 0)
        {
            var message = TcpSocketUtility.ReceiveString(serviceSocket, OnReceiveDataSizeCheckFail, OnReceiveDataCheckFail);
            var messageObject = JsonSerializer.Deserialize<BaseMessage>(message);
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
}