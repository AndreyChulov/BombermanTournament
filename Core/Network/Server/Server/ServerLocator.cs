using System.Net;
using System.Net.Sockets;

namespace Core.Network.Server.Server
{
    internal class ServerLocator : IDisposable
    {
        private bool _isStarted;
        private Thread _serverLocatorSenderThread;
        private Thread _serverLocatorResieverThread;
        private Socket _udpBroadcastSocket;

        public ServerLocator()
        {
            _isStarted = false;

            _serverLocatorSenderThread = new Thread(ServerLocatorSender);
            _serverLocatorResieverThread = new Thread(ServerLocatorReceiver);

            _udpBroadcastSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _udpBroadcastSocket.EnableBroadcast = true;
        }

        public void Start()
        {
            _isStarted = true;

            _serverLocatorSenderThread.Start();
            _serverLocatorResieverThread.Start();
        }

        public void Stop()
        {
            _isStarted = false;

            Task.Delay(100).Wait();

            _serverLocatorResieverThread.Abort();
            _serverLocatorSenderThread.Abort();
        }

        private void ServerLocatorSender()
        {
            //IPAddress broadcastAddress = CreateBroadcastAddress();
            //var broadcastIpEndPoint = new IPEndPoint(broadcastAddress, 11111);
            //_udpBroadcastSocket.Connect(broadcastIpEndPoint);

            while (_isStarted)
            {
                //SocketUtility.SendString(_udpBroadcastSocket, "Follow the white rabbit!", () => { });
                Task.Delay(1000).Wait();
            }
        }

        private void ServerLocatorReceiver()
        {
            _udpBroadcastSocket.Bind(new IPEndPoint(IPAddress.Any, 11111));

            while (_isStarted)
            {
                if (_udpBroadcastSocket.Available > 0)
                {
                    Task.Delay(100).Wait();
                }
                Task.Delay(1000).Wait();
            }
        }

        public void Dispose()
        {
            Stop();
            _udpBroadcastSocket.Dispose();
        }
    }
}
