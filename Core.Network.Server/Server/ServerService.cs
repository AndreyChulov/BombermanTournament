using System.Data;
using System.Net;
using System.Net.Sockets;
using Core.Network.ExternalShared;
using Core.Network.InternalShared;

namespace Core.Network.Server.Server
{
    public class ServerService :BaseThreadService
    {
        private readonly Action _onClientConnected;
        public int ServerPort { get; }
        public List<ConnectedClientService> ConnectedClientServices { get; }
        
        public ServerService(Action onClientConnected) : base(NetworkSettings.WaitForClientConnectionTimeout)
        {
            _onClientConnected = onClientConnected;
            ConnectedClientServices = new List<ConnectedClientService>();
            
            Random randomGenerator = new Random();
            
            ServerPort = NetworkSettings.ServerTcpPorts[randomGenerator.Next(0, NetworkSettings.ServerTcpPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Parse(IpAddressUtility.GetLocalIpAddress()), ServerPort));
            socket.Listen();
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket? serviceSocket)
        {
            if (serviceSocket == null)
            {
                throw new InvalidConstraintException(
                    $"${nameof(ServerService)}.{nameof(ServiceWorkerLoop)} can not work with null {nameof(serviceSocket)}");
            }

            try
            {
                var clientSocket = serviceSocket.Accept();
                var connectedClientService = new ConnectedClientService(clientSocket);
                connectedClientService.Start();
                ConnectedClientServices.Add(connectedClientService);
                
                Task.Run(() => _onClientConnected());
            }
            catch (SocketException exception)
            {
                if (exception.ErrorCode != 10035)//10035 mean no clients wait's for connection
                {
                    throw;
                }
            }
        }
    }
}