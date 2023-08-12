using System;
using System.Collections.Generic;
using System.Net;
using ImageChat.Shared;

namespace ImageChat.Server.Server
{
    public class ServerLocatorService : IDisposable
    {
        private readonly int _serverServicePort;
        private List<string> _servers;
        private readonly ServerLocatorSenderService _serverLocatorSenderService;
        private readonly ServerLocatorReceiverService _serverLocatorReceiverService;

        public ServerLocatorService(int serverServicePort)
        {
            _serverServicePort = serverServicePort;
            _servers = new List<string>();
            _serverLocatorSenderService = new ServerLocatorSenderService(TimeSpan.FromSeconds(0.5f));
            _serverLocatorReceiverService = new ServerLocatorReceiverService(
                Constants.ServerLocatorBroadcastDatagramReceiveTimeout);
            
            _serverLocatorReceiverService.BroadcastMessageReceived += 
                ServerLocatorReceiverService_OnBroadcastMessageReceived;
        }

        private void ServerLocatorReceiverService_OnBroadcastMessageReceived(object sender, string e)
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
        }

        public void Start()
        {
            _serverLocatorSenderService.Start();
            _serverLocatorReceiverService.Start();
            Logger.AddVerboseMessage("Server locator service started");
        }

        public void Stop()
        {
            _serverLocatorSenderService.Stop();
            _serverLocatorReceiverService.Stop();
            Logger.AddVerboseMessage("Server locator service stopped");
        }

        public void Dispose()
        {
            _serverLocatorSenderService?.Dispose();
            _serverLocatorReceiverService?.Dispose();
            Logger.AddVerboseMessage("Server locator service disposed");
        }
    }
}