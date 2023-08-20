﻿using System.Net;
using System.Net.Sockets;
using Core.Network.ExternalShared;
using Core.Network.InternalShared;

namespace Core.Network.Client.Client
{
    public class ServerLocatorReceiverService : BaseThreadService
    {
        public event EventHandler<string> UdpMessageReceived; 
        public int UdpPort { get; }
        
        public ServerLocatorReceiverService(TimeSpan loopDelay) : base(loopDelay)
        {
            Random randomGenerator = new Random();
            UdpPort = NetworkSettings.ServerLocatorUdpPorts[randomGenerator.Next(0, NetworkSettings.ServerLocatorUdpPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            
            socket.Bind(new IPEndPoint(IPAddress.Any, UdpPort));
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            if (serviceSocket.Available > 0)
            {
                var datagram = new byte[NetworkSettings.UdpDatagramSize];

                using (var asyncState = new SocketAsyncState(serviceSocket))
                {
                    serviceSocket.BeginReceive(datagram, 0, NetworkSettings.UdpDatagramSize, SocketFlags.None,
                                        serviceSocketReceive_Callback, asyncState);

                    asyncState.ManualResetEvent.WaitOne();
                }

                var serverMessage = UdpSocketUtility.GetStringFromDatagram(datagram);
                UdpMessageReceived?.Invoke(this, serverMessage);
            }
        }

        private void serviceSocketReceive_Callback(IAsyncResult asyncInfo)
        {
            SocketAsyncState asyncState = (SocketAsyncState)asyncInfo.AsyncState;
            
            asyncState.Socket.EndReceive(asyncInfo);

            asyncState.ManualResetEvent.Set();
        }
    }
}