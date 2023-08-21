namespace Core.Network.ExternalShared
{
    public static class NetworkSettings
    {
        public static int[] ServerLocatorBroadcastPorts => new[] 
            {31110, 31111, 31112, 31113, 31114, 31115, 31116, 31117, 31118, 31119};
        public static int[] ClientLocatorUdpPorts => new []
            {21210, 21211, 21212, 21213, 21214, 21215, 21216, 21217, 21218, 21219};
        public static int[] ServerTcpPorts => new []
            {11110, 11111, 11112, 11113, 11114, 11115, 11116, 11117, 11118, 11119};
        public static int UdpDatagramSize => 1024;
        public static TimeSpan ServerLocatorBroadcastDatagramSendTimeout => TimeSpan.FromSeconds(1);
        public static TimeSpan ServerLocatorQueueCheckTimeout => TimeSpan.FromSeconds(0.5f);
        public static TimeSpan ClientLocatorQueueCheckTimeout => TimeSpan.FromSeconds(0.5f);
        public static TimeSpan WaitForClientConnectionTimeout => TimeSpan.FromSeconds(1);
        public static TimeSpan ClientQueueCheckTimeout => TimeSpan.FromSeconds(0.1f);
        public static string ServerLogsFile => "Logs/server.log";
        public static string ClientLogsFile => "Logs/client.log";
        public static int MaxUnarchivedLogFileSize => 20;
    }
}