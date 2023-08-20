using System.Net;
using System.Net.Sockets;
using Core.Network.ExternalShared;
using Core.Network.InternalShared;

namespace Core.Network.Server.Server
{
    public class ServerLocatorReceiverService : BaseThreadService
    {
        public event EventHandler<string> BroadcastMessageReceived; 
        private readonly int _bindingPort;
        
        public ServerLocatorReceiverService(TimeSpan loopDelay) : base(loopDelay)
        {
            Random randomGenerator = new Random();
            
            _bindingPort = NetworkSettings.ServerLocatorBroadcastPorts
                    [randomGenerator.Next(0, NetworkSettings.ServerLocatorBroadcastPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.EnableBroadcast = true;
            socket.Bind(new IPEndPoint(IPAddress.Any, _bindingPort));
            
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            if (serviceSocket.Available == 0)
            {
                return;
            }

            var message = ReceiveMessage(serviceSocket).Result;

            BroadcastMessageReceived?.Invoke(this, message);
        }

        private static async Task<string> ReceiveMessage(Socket serviceSocket)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[NetworkSettings.UdpDatagramSize]);

            await serviceSocket.ReceiveAsync(buffer, SocketFlags.None);

            return UdpSocketUtility.GetStringFromDatagram(buffer.Array);
        }
    }
}