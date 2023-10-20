namespace Core.Network.Shared.Contracts;

public class ServerInfoDatagram
{
    public string Message { get; set; }
    public string TcpServerIP { get; set; }
    public string TcpServerPort { get; set; }
}