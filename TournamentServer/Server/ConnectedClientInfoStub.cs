using Core.Network.ExternalShared.Contracts;

namespace TournamentServer.Server;

public class ConnectedClientInfoStub : ConnectedClient, IConnectedClientInfo
{
    public ConnectedClientInfoStub() 
        : base(new ConnectedClientId("unknown", -1))
    {
    }
}