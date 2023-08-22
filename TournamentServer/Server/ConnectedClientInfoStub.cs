namespace TournamentServer.Server;

public class ConnectedClientInfoStub : IConnectedClientInfo
{
    public bool Equals(IConnectedClientInfo? other)
    {
        return true;
    }

}