using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Core.Network.ExternalShared;
using Core.Network.InternalShared;

namespace Core.Network.Client.Client
{
    public class ClientLocatorReceiverService : BaseThreadService
    {
        private Action<string> _udpMessageReceived; 
        
        public ClientLocatorReceiverService(Action<string> udpMessageReceived) 
            : base(NetworkSettings.ClientLocatorQueueCheckTimeout)
        {
            _udpMessageReceived = udpMessageReceived;
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

            var occupiedPorts = IPGlobalProperties
                                        .GetIPGlobalProperties()
                                        .GetActiveUdpListeners()
                                        .Select(x => x.Port)
                                        .ToArray();
            var portToOccupy = NetworkSettings.ClientLocatorUdpPorts
                .First(x => occupiedPorts.All(occupied => x != occupied));
            
            socket.Bind(new IPEndPoint(IPAddress.Any, portToOccupy));
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket? serviceSocket)
        {
            if (serviceSocket == null)
            {
                throw new InvalidConstraintException(
                    $"${nameof(ClientLocatorReceiverService)}.{nameof(ServiceWorkerLoop)} can not work with null {nameof(serviceSocket)}");
            }
            
            if (serviceSocket.Available > 0)
            {
                var datagram = new byte[NetworkSettings.UdpDatagramSize];

                serviceSocket.Receive(datagram, 0, NetworkSettings.UdpDatagramSize, SocketFlags.None);

                var serverMessage = UdpSocketUtility.GetStringFromDatagram(datagram);
                
                _udpMessageReceived(serverMessage);
            }
        }
    }
}