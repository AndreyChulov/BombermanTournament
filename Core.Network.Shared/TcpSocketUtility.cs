using System.Net.Sockets;
using Core.Network.Shared.Utilities;

namespace Core.Network.Shared
{
    public static class TcpSocketUtility
    {
        public static string ReceiveString(Socket socket, 
            Action onReceiveDataSizeCheckFail, Action onReceiveDataCheckFail)
        {
            return TcpSocketStringReceiver.ReceiveString(socket, onReceiveDataSizeCheckFail, onReceiveDataCheckFail);
        }

        public static void SendString(Socket socket, string dataToSend)
        {
            TcpSocketStringSender.SendString(socket, dataToSend);
        }
    }
}