using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.ExternalShared.Contracts.Extensions;
using Core.Network.ExternalShared.Contracts.Messages;
using Core.Network.InternalShared;

namespace Core.Network.Client.Client;

public class ClientService : BaseThreadService
{
    private readonly string _serverIp;
    private readonly int _serverPort;
    private readonly Action<BaseMessage?, string> _onMessageReceived;
    private Socket _clientSocket;

    public ClientService(string serverIP, int serverPort, Action<BaseMessage?, string> onMessageReceived) : base(NetworkSettings.ClientQueueCheckTimeout)
    {
        _serverIp = serverIP;
        _serverPort = serverPort;
        _onMessageReceived = onMessageReceived;
        
        _clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
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

        //serviceSocket.Poll(10, SelectMode.SelectRead);
        if (serviceSocket.Available > 0)
        {
            //serviceSocket.Blocking = true;
            var message = TcpSocketUtility.ReceiveString(serviceSocket, OnReceiveDataSizeCheckFail, OnReceiveDataCheckFail);
            //serviceSocket.Blocking = false;
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