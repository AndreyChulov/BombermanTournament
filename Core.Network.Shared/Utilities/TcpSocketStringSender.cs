using System.Net.Sockets;

namespace Core.Network.Shared.Utilities
{
    internal static class TcpSocketStringSender
    {
        public static void SendString(Socket socket, string dataToSend)
        {
            using (Stream dataStream = new MemoryStream())
            using (BinaryWriter dataStreamWriter = new BinaryWriter(dataStream))
            {
                SocketSender.WriteDataPacketToStream(dataToSend, dataStreamWriter, dataStream);

                var sendDataBuffer = SocketSender.GetSendDataBuffer(() => {}, dataStream);

                socket.Send(sendDataBuffer);
            }
        }

    }
}