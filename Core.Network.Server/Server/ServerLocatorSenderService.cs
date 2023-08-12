using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ImageChat.Protocol;
using ImageChat.Shared;

namespace ImageChat.Server.Server
{
    public class ServerLocatorSenderService : BaseThreadService
    {
        private readonly Queue<KeyValuePair<IPEndPoint, string>> _messagesToSend;
        private readonly object _messagesToSendLockObject;
        
        public ServerLocatorSenderService(TimeSpan loopDelay) : base(loopDelay)
        {
            _messagesToSend = new Queue<KeyValuePair<IPEndPoint, string>>();
            _messagesToSendLockObject = new object();
        }

        public void SendInfo(IPEndPoint targetLocatorServiceEndPoint, string message)
        {
            lock (_messagesToSendLockObject)
            {
                _messagesToSend.Enqueue(new KeyValuePair<IPEndPoint, string>(targetLocatorServiceEndPoint, message));
            }
            Logger.AddTypedVerboseMessage(GetType(), 
                $@"Message[{message}] with target endpoint" +
                $@"[{targetLocatorServiceEndPoint.Address.MapToIPv4()}:" + 
                $@"{targetLocatorServiceEndPoint.Port}] enqueued to send messages queue.");
        }
        
        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

            socket.EnableBroadcast = true;
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            lock (_messagesToSendLockObject)
            {
                if (!_messagesToSend.Any())
                {
                    return;
                }
                
                StartSendMessages(serviceSocket).Wait();
            }
            
            Logger.AddTypedVerboseMessage(GetType(), @"All enqueue messages sent.");
       }

        private async Task StartSendMessages(Socket serviceSocket)
        {
            while (_messagesToSend.Any())
            {
                var messageToSend = _messagesToSend.Dequeue();
                var datagramArray =
                    UdpSocketUtility.PrepareDatagramForSendingString(
                        Constants.UdpDatagramSize, 
                        messageToSend.Value,
                        () => throw new ArgumentOutOfRangeException(
                            $"Can not send string, data size exceeds datagram size")
                    );
                var datagram = new ArraySegment<byte>(datagramArray);
                
                await serviceSocket.SendToAsync(datagram, SocketFlags.None, messageToSend.Key);
            }
        }
        
    }
}