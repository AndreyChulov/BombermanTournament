using System.Net.Sockets;

namespace Core.Network.Shared
{
    public class SocketAsyncState : IDisposable
    {
        public Socket Socket { get; }
        public ManualResetEvent ManualResetEvent { get; }

        public SocketAsyncState(Socket socket)
        {
            Socket = socket;
            ManualResetEvent = new ManualResetEvent(false);
        }

        public void Dispose()
        {
            ManualResetEvent?.Dispose();
        }
    }
}