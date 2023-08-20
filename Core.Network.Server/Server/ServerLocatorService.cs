using System.Net;
using System.Net.Sockets;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.InternalShared;

namespace Core.Network.Server.Server
{
    public class ServerLocatorService : BaseThreadService
    {
        private readonly int _serverServicePort;
        private List<string> _servers;
        private readonly ServerLocatorSenderService _serverLocatorSenderService;
        //private readonly ServerLocatorReceiverService _serverLocatorReceiverService;

        public ServerLocatorService(int serverServicePort)
        {
            _serverServicePort = serverServicePort;
            _servers = new List<string>();
            _serverLocatorSenderService = new ServerLocatorSenderService(TimeSpan.FromSeconds(0.5f));
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
            var serverInfoDatagram = new ServerInfoDatagram
            {
                Message = "Tournament server info",
                TcpServerPort = _serverServicePort.ToString(),
                TcpServerIP = IpAddressUtility.GetLocalIpAddress()
            };
            
            foreach (var port in NetworkSettings.ServerLocatorBroadcastPorts)
            {
                _serverLocatorSenderService.SendInfo(new IPEndPoint(IPAddress.Any, port),
                    "$Tournament server info:[{IpAddressUtility.GetLocalIpAddress()}:{_serverServicePort}]");
            }
        }

        public void Dispose()
        {
            _serverLocatorSenderService.Dispose();
            //_serverLocatorReceiverService?.Dispose();
        }
    }
}