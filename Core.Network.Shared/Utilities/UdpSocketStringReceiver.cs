using System.IO;

namespace ImageChat.Protocol.Utilities
{
    internal static class UdpSocketStringReceiver
    {
        public static string GetStringFromDatagram(byte[] datagram)
        {
            using (Stream dataStream = new MemoryStream(datagram))
            using (BinaryReader dataStreamReader = new BinaryReader(dataStream))
            {
                dataStream.Seek(0, SeekOrigin.Begin);
                var dataSize = dataStreamReader.ReadInt64();
                
                dataStream.SetLength(dataSize + sizeof(long));
                
                dataStream.Seek(sizeof(long), SeekOrigin.Begin);
                return dataStreamReader.ReadString();
            }
        }
    }
}