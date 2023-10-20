using Core.Network.InternalShared.Utilities;

namespace Core.Network.InternalShared
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