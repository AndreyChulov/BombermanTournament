using System;
using System.IO;
using System.Net.Sockets;

namespace ImageChat.Protocol.Utilities
{
    internal static class TcpSocketStringReceiver
    {
        public static string ReceiveString(Socket socket, 
            Action onReceiveDataSizeCheckFail, Action onReceiveDataCheckFail)
        {
            using (Stream dataStream = new MemoryStream())
            using (BinaryReader dataStreamReader = new BinaryReader(dataStream))
            {
                var dataSize = ReceiveDataSize(socket, dataStream, dataStreamReader, onReceiveDataSizeCheckFail);
                ReceiveDataToStream(socket, dataSize, dataStream, onReceiveDataCheckFail);
                
                dataStream.Seek(0, SeekOrigin.Begin);
                return dataStreamReader.ReadString();
            }
        }
        
        private static void ReceiveDataToStream(
            Socket socket, long dataSize, 
            Stream dataStream, Action onReceiveDataCheckFail)
        {
            var maxBufferSize = 1024;
            var remainingDataSize = dataSize;

            dataStream.Seek(0, SeekOrigin.Begin);
            
            while (remainingDataSize > maxBufferSize)
            {
                ReceiveBufferToStream(socket, dataStream, maxBufferSize, onReceiveDataCheckFail);

                remainingDataSize -= maxBufferSize;
            }
            
            ReceiveBufferToStream(socket, dataStream, (int)remainingDataSize, onReceiveDataCheckFail);
        }

        private static void ReceiveBufferToStream(
            Socket socket, Stream dataStream, int bufferSize,
            Action onReceiveDataCheckFail)
        {
            TcpSocketReceiver.WaitDataFromSocket(socket, bufferSize);

            byte[] dataBuffer = new byte[bufferSize];
            var receivedBufferSize = socket.Receive(dataBuffer);

            if (receivedBufferSize != bufferSize)
            {
                onReceiveDataCheckFail();
            }

            dataStream.Write(dataBuffer, 0, bufferSize);
        }

        private static long ReceiveDataSize(Socket socket, Stream dataStream, 
            BinaryReader dataStreamReader, Action onReceiveDataCheckFail)
        {
            TcpSocketReceiver.WaitDataFromSocket(socket, sizeof(long));

            byte[] dataBuffer = new byte[sizeof(long)];
            var receivedBufferSize = socket.Receive(dataBuffer);

            if (receivedBufferSize != dataBuffer.Length)
            {
                onReceiveDataCheckFail();
            }

            dataStream.Seek(0, SeekOrigin.Begin);
            dataStream.Write(dataBuffer, 0, dataBuffer.Length);
            dataStream.Seek(0, SeekOrigin.Begin);
            return dataStreamReader.ReadInt64();
        }
    }
}