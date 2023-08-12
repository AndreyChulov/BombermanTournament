using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ImageChat.Shared
{
    public static class IpAddressUtility
    {
        public static IPAddress GetBroadcastAddress()
        {
            var localIpAddress = GetLocalIpAddress();

            var localIpAddressNumbers = localIpAddress.Split('.');
            
            localIpAddressNumbers[3] = "255";
            
            var remoteIpAddressInString = localIpAddressNumbers
                .Aggregate("", (acc, value) => $"{acc}.{value}")
                .Substring(1);
            
            var broadcastAddress = IPAddress.Parse(remoteIpAddressInString);
            
            return broadcastAddress;
        }

        public static string GetLocalIpAddress()
        {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == AddressFamily.InterNetwork)
                .ToString();
        }

    }
}