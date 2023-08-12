using System;
using System.Net.Sockets;
using ImageChat.Shared;

namespace ImageChat.Server.Server
{
    public class ServerService :BaseThreadService
    {
        public int TcpPort { get; }
        
        public ServerService(TimeSpan loopDelay) : base(loopDelay)
        {
            Random randomGenerator = new Random();
            
            TcpPort = Constants.ServerTcpPorts[randomGenerator.Next(0, Constants.ServerTcpPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            throw new NotImplementedException();
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            throw new NotImplementedException();
        }
    }
}