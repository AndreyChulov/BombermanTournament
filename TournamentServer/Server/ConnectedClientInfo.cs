using Core.Network.ExternalShared.Contracts;

namespace TournamentServer.Server;

public class ConnectedClientInfo : ConnectedClient, IConnectedClientInfo
{
    public ConnectedClientInfo(ConnectedClient connectedClient) : base(connectedClient.ConnectedClientId)
    {
    }
}