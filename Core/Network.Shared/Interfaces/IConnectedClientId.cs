namespace Core.Network.Shared.Interfaces;

public interface IConnectedClientId
{
    string ClientIP { get; }
    int ClientPort { get; }
}