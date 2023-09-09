using Core.Network;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using Core.Network.InternalShared;

namespace Player1DumbBot;

public class Client : IDisposable
{
    private readonly INetworkClientObject _networkClient;

    public Client()
    {
        Logger.Initialize(NetworkSettings.ClientLogsFiles);
        _networkClient = NetworkFactory.CreateNetworkObject<INetworkClientObject>(NetworkObjectType.Client,
            OnNetworkClentCreated, OnNetworkClientDestroyed);
        _networkClient.StartClient();
    }

    private void OnNetworkClientDestroyed()
    {
        throw new NotImplementedException();
    }

    private void OnNetworkClentCreated()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _networkClient.StopClient();
        Logger.FreeUpResources();
        // TODO release managed resources here
    }
}