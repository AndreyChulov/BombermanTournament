using Core.Network.Shared.Utilities;

namespace Core.Network.Shared
{
    public static class UdpSocketUtility
    {
        public static string GetStringFromDatagram(byte[] datagram)
        {
            return UdpSocketStringReceiver.GetStringFromDatagram(datagram);
        }

        public static byte[] PrepareDatagramForSendingString(int datagramSize, string dataToSend, 
            Action onDatagramSizeCheckFail)
        {
            return  UdpSocketStringSender.PrepareDatagramForSendingString(datagramSize, dataToSend, onDatagramSizeCheckFail);
        }
    }
}