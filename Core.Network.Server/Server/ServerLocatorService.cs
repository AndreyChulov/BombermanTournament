using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.InternalShared;

namespace Core.Network.Server.Server
{
    public class ServerLocatorService : BaseThreadService
    {
        private readonly ServerLocatorSenderService _serverLocatorSenderService;
        private readonly string _serverInfoDatagramSerialized;
        //private readonly ServerLocatorReceiverService _serverLocatorReceiverService;
        private List<string> _servers;

        public ServerLocatorService(int serverServicePort) 
            : base(NetworkSettings.ServerLocatorBroadcastDatagramSendTimeout)
        {
            _servers = new List<string>();
            _serverLocatorSenderService = new ServerLocatorSenderService();
            
            var serverInfoDatagram = new ServerInfoDatagram
            {
                Message = "Tournament server info",
                TcpServerPort = serverServicePort.ToString(),
                TcpServerIP = IpAddressUtility.GetLocalIpAddress()
            };

            _serverInfoDatagramSerialized = JsonSerializer.Serialize(serverInfoDatagram);
            //_serverLocatorReceiverService = new ServerLocatorReceiverService(
            //    NetworkSettings.ServerLocatorBroadcastDatagramReceiveTimeout);
            
            //_serverLocatorReceiverService.BroadcastMessageReceived += 
            //    ServerLocatorReceiverService_OnBroadcastMessageReceived;
        }

        /*private void ServerLocatorReceiverService_OnBroadcastMessageReceived(object sender, string e)
        {
            Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} -> [ServerLocatorSenderService] " + 
                              $@"Server received broadcast message [{e}]");
            var data = e.Split(new[] { '[', ']', ':' }, StringSplitOptions.RemoveEmptyEntries);
            var clientIp = data[0];
            var clientPort = data[1];
            var clientRequest = data[2];

            switch (clientRequest)
            {
                case "Get image chat server IP&Port":
                    _serverLocatorSenderService.SendInfo(
                        new IPEndPoint(IPAddress.Parse(clientIp), Convert.ToInt32(clientPort)),
                        $"[{IpAddressUtility.GetLocalIpAddress()}:{_serverServicePort}]Server info"
                    );
                    break;
                default:
                    Logger.AddTypedVerboseMessage(GetType(), "Unknown command received from broadcast");
                    break;
                    
            }
        }*/

        protected override Socket? CreateServiceSocket()
        {
            return null;
        }

        public override void Start()
        {
            _serverLocatorSenderService.Start();
            //_serverLocatorReceiverService.Start();
            base.Start();
            Logger.AddVerboseMessage("Server locator service started");
        }

        public override void Stop()
        {
            _serverLocatorSenderService.Stop();
            //_serverLocatorReceiverService.Stop();
            base.Stop();
            Logger.AddVerboseMessage("Server locator service stopped");
        }

        protected override void ServiceWorkerLoop(Socket? serviceSocket)
        {
            foreach (var port in NetworkSettings.ClientLocatorUdpPorts)
            {
                _serverLocatorSenderService.SendInfo(
                    new IPEndPoint(IPAddress.Broadcast, port),
                    _serverInfoDatagramSerialized);
            }
        }

        public void Dispose()
        {
            _serverLocatorSenderService.Dispose();
            //_serverLocatorReceiverService?.Dispose();
        }
    }
}