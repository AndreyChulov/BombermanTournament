using System;
using System.Net.Sockets;
using ImageChat.Protocol.Utilities;

namespace ImageChat.Protocol
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