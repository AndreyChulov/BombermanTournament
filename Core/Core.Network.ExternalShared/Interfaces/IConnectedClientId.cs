namespace Core.Network.ExternalShared.Interfaces;

public interface IConnectedClientId
{
    string ClientIP { get; }
    int ClientPort { get; }
}