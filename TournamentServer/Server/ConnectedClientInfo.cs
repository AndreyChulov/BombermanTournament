using Core.Network.ExternalShared.Contracts;
using Core.Network.ExternalShared.Interfaces;

namespace TournamentServer.Server;

public class ConnectedClientInfo : ConnectedClient
{
    public ConnectedClientInfo(ConnectedClient connectedClient) : base(connectedClient.ConnectedClientId)
    {
    }
}