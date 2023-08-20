namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkClientObject : INetworkObject
{
    void StartClient();
    void StopClient();
}