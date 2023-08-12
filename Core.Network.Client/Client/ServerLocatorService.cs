using System;
using System.Collections.Generic;
using System.Net;
using ImageChat.Shared;

namespace ImageChat.Client.Client
{
    public class ServerLocatorService : IDisposable
    {
        public List<IPEndPoint> Servers { get; }
        private readonly ServerLocatorSenderService _serverLocatorSenderService;
        private readonly ServerLocatorReceiverService _serverLocatorReceiverService;

        public ServerLocatorService()
        {
            Servers = new List<IPEndPoint>();
            _serverLocatorReceiverService = new ServerLocatorReceiverService(TimeSpan.FromSeconds(0.5f));
            _serverLocatorSenderService = new ServerLocatorSenderService(
                Constants.ServerLocatorBroadcastDatagramSendTimeout, 
                Constants.ServerLocatorBroadcastPorts,
                _serverLocatorReceiverService.UdpPort);
            
            _serverLocatorReceiverService.UdpMessageReceived += ServerLocatorReceiverService_OnUdpMessageReceived;
        }

        private void ServerLocatorReceiverService_OnUdpMessageReceived(object sender, string e)
        {
             var serverData = e.Split(new[] { '[', ']', ':' }, StringSplitOptions.RemoveEmptyEntries);
             var serverIp = serverData[0];
             var serverPort = serverData[1];
             var serverMessage = serverData[2];

             switch (serverMessage)
             {
                 case "Server info":
                     var server = new IPEndPoint(IPAddress.Parse(serverIp), Convert.ToInt32(serverPort));

                     if (!Servers.Contains(server))
                     {
                         Servers.Add(server);
                     }
                     
                     break;
                 default:
                     Logger.AddTypedVerboseMessage(GetType(), @"Unknown message received from udp");
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