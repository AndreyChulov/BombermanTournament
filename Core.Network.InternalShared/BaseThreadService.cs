using System.Net.Sockets;

namespace Core.Network.InternalShared
{
    public abstract class BaseThreadService : IDisposable
    {
        private readonly TimeSpan _loopDelay;
        private bool _isStarted;
        private readonly Thread _serviceThread;

        protected BaseThreadService(TimeSpan loopDelay)
        {
            _loopDelay = loopDelay;
            _isStarted = false;
            _serviceThread = new Thread(ServiceWorker);
        }

        protected abstract Socket? CreateServiceSocket();
        
        public virtual void Start()
        {
            _isStarted = true;

            _serviceThread.Start();
        }

        public virtual void Stop()
        {
            _isStarted = false;

            while (_serviceThread.IsAlive)
            {
                Task.Delay(_loopDelay).Wait();
            }
        }

        private void ServiceWorker()
        {
            using (var serviceSocket = CreateServiceSocket())
            {
                while (_isStarted)
                {
                    ServiceWorkerLoop(serviceSocket);
                
                    Task.Delay(_loopDelay).Wait();
                }
                
                serviceSocket.Close();
            }
        }

        protected abstract void ServiceWorkerLoop(Socket? serviceSocket);
        
        public virtual void Dispose()
        {
            Stop();
        }
    }
}