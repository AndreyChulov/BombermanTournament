using System;
using System.Threading.Tasks;
using Core.Network.ExternalShared.Contracts;

namespace TournamentServer.Server;

public class ConnectedClientInfo : ConnectedClient, IConnectedClientInfo
{
    private Action? _onClientUpdated;
    
    public ConnectedClientInfo(ConnectedClient connectedClient) : base(connectedClient.ConnectedClientId)
    {
        _onClientUpdated = null;
    }
    
    public void OnClientUpdated()
    {
        if (_onClientUpdated == null)
        {
            return;
        }

        Task.Run(() => _onClientUpdated());
    }

    public void SetOnClientUpdatedAction(Action onClientUpdated)
    {
        _onClientUpdated = onClientUpdated;
    }
}