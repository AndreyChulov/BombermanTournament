using System.Data;
using System.Net;
using System.Text.Json;
using Core.Network.ExternalShared.Contracts;
using Core.Network.InternalShared;

namespace Core.Network.Client.Client
{
    public class ClientLocatorService : IDisposable
    {
        private readonly Action<List<IPEndPoint>> _onServerAdded;
        private readonly List<IPEndPoint> _servers;
        //private readonly ServerLocatorSenderService _serverLocatorSenderService;
        private readonly ClientLocatorReceiverService _clientLocatorReceiverService;

        public ClientLocatorService(Action<List<IPEndPoint>> onServerAdded)
        {
            _onServerAdded = onServerAdded;
            _servers = new List<IPEndPoint>();
            _clientLocatorReceiverService = new ClientLocatorReceiverService(
                ClientLocatorReceiverService_OnUdpMessageReceived);
            /*_serverLocatorSenderService = new ServerLocatorSenderService(
                NetworkSettings.ServerLocatorBroadcastDatagramSendTimeout, 
                NetworkSettings.ServerLocatorBroadcastPorts,
                _serverLocatorReceiverService.UdpPort);*/
        }

        private void ClientLocatorReceiverService_OnUdpMessageReceived(string jsonMessage)
        {
            var messageObject = JsonSerializer.Deserialize<ServerInfoDatagram>(jsonMessage);

            if (messageObject == null)
            {
                throw new InvalidConstraintException($"Received understandable {nameof(jsonMessage)} => {jsonMessage}");
            }

            switch (messageObject.Message)
            {
                case "Tournament server info":
                    var server = new IPEndPoint(
                        IPAddress.Parse(messageObject.TcpServerIP), 
                        Convert.ToInt32(messageObject.TcpServerPort)
                        );

                    if (!_servers.Contains(server))
                    {
                        _servers.Add(server);
                        _onServerAdded(_servers);
                    }

                    break;
                default:
                    Logger.AddVerboseMessage(@"Unknown message received from udp");
                    break;
            }
        }

        public void Start()
        {
            //_serverLocatorSenderService.Start();
            _clientLocatorReceiverService.Start();
            Logger.AddVerboseMessage("Client locator service started");
        }

        public void Stop()
        {
            //_serverLocatorSenderService.Stop();
            _clientLocatorReceiverService.Stop();
            Logger.AddVerboseMessage("Client locator service stopped");
        }

        public void Dispose()
        {
            //_serverLocatorSenderService?.Dispose();
            _clientLocatorReceiverService.Dispose();
            Logger.AddVerboseMessage("Client locator service disposed");
        }
    }
}