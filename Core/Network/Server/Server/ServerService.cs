﻿using System.Data;
using System.Net;
using System.Net.Sockets;
using Core.Network.InternalShared;
using Core.Network.Shared;
using Core.Network.Shared.Contracts;

namespace Core.Network.Server.Server
{
    public class ServerService :BaseThreadService
    {
        private readonly Action _onClientConnected;
        private readonly Action<ConnectedClientId> _onClientUpdated;
        private readonly Action<ConnectedClientId> _onClientDisconnected;
        public int ServerPort { get; }
        public string ServerIp { get; private set; }
        public List<ConnectedClientService> ConnectedClientServices { get; }
        private object _connectedClientServicesLockObject = new object();
        
        public ServerService(Action onClientConnected, 
            Action<ConnectedClientId> onClientUpdated,
            Action<ConnectedClientId> onClientDisconnected) 
            : base(NetworkSettings.WaitForClientConnectionTimeout)
        {
            _onClientConnected = onClientConnected;
            _onClientUpdated = onClientUpdated;
            _onClientDisconnected = onClientDisconnected;
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
                
                lock (_connectedClientServicesLockObject)
                {
                    var connectedClientService = new ConnectedClientService(clientSocket, 
                        OnConnectedClientUpdated, OnConnectedClientDisconnected);
                    connectedClientService.Start();
                    ConnectedClientServices.Add(connectedClientService);
                }
                
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

        private void OnConnectedClientDisconnected(ConnectedClientId connectedClientId)
        {
            lock (_connectedClientServicesLockObject)
            {
                var disconnectedClientService = ConnectedClientServices
                    .FirstOrDefault(x => x.ConnectedClient.ConnectedClientId.Equals(connectedClientId));
            
                ConnectedClientServices.Remove(disconnectedClientService);
                disconnectedClientService?.Dispose();
            }
            
            Task.Run(() => _onClientDisconnected(connectedClientId));
        }

        private void OnConnectedClientUpdated(ConnectedClientId connectedClientId)
        {
            Task.Run(() => _onClientUpdated(connectedClientId));
        }

        public override void Dispose()
        {
            base.Dispose();

            lock (_connectedClientServicesLockObject)
            {
                foreach (var connectedClientService in ConnectedClientServices)
                {
                    connectedClientService.Dispose();
                }
            
                ConnectedClientServices.Clear();
            }
        }
    }
}