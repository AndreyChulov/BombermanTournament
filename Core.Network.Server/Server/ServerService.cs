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
        private readonly Action _onClientUpdated;
        public int ServerPort { get; }
        public string ServerIp { get; private set; }
        public List<ConnectedClientService> ConnectedClientServices { get; }
        
        public ServerService(Action onClientConnected, Action onClientUpdated) 
            : base(NetworkSettings.WaitForClientConnectionTimeout)
        {
            _onClientConnected = onClientConnected;
            _onClientUpdated = onClientUpdated;
            ConnectedClientServices = new List<ConnectedClientService>();
            
            
            Random randomGenerator = new Random();
            
            ServerIp = IpAddressUtility.GetLocalIpAddress();
            ServerPort = NetworkSettings.ServerTcpPorts[randomGenerator.Next(0, NetworkSettings.ServerTcpPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            socket.Blocking = false;
            
            socket.Bind(new IPEndPoint(IPAddress.Parse(ServerIp), ServerPort));
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
                var connectedClientService = new ConnectedClientService(clientSocket, OnConnectedClientUpdated);
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
        
        private void OnConnectedClientUpdated()
        {
            Task.Run(() => _onClientUpdated());
        }
    }
}