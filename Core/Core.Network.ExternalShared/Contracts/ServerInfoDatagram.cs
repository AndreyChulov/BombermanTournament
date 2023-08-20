namespace Core.Network.ExternalShared.Contracts;

public class ServerInfoDatagram
{
    public string Message { get; set; }
    public string TcpServerIP { get; set; }
    public string TcpServerPort { get; set; }
}