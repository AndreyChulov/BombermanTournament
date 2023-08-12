using System;
using System.IO;

namespace ImageChat.Protocol.Utilities
{
    internal static class UdpSocketStringSender
    {
        public static byte[] PrepareDatagramForSendingString(int datagramSize, string dataToSend, 
            Action onDatagramSizeCheckFail)
        {
            using (Stream dataStream = new MemoryStream(datagramSize))
            using (BinaryWriter dataStreamWriter = new BinaryWriter(dataStream))
            {
                SocketSender.WriteDataPacketToStream(dataToSend, dataStreamWriter, dataStream);

                if (dataStream.Length > datagramSize)
                {
                    onDatagramSizeCheckFail();
                }
                
                return ReadDatagramFromStream(dataStream, datagramSize);
            }
        }

        private static byte[] ReadDatagramFromStream(Stream dataStream, int datagramSize)
        {
            var datagram = new byte[datagramSize];

            dataStream.Seek(0, SeekOrigin.Begin);
            dataStream.Read(datagram, 0, datagramSize);

            return datagram;
        }
    }
}