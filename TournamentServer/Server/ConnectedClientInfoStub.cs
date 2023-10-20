using System;
using Core.Network.Shared.Contracts;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class ConnectedClientInfoStub : ConnectedClient, IConnectedClientInfo
{
    public MonitoredVariable<bool> IsReadyForTournamentStart => false;
    public MonitoredVariable<string> NickName => "unknown";
    public MonitoredVariable<string> StrategyDescription => "unknown";

    public ConnectedClientInfoStub() 
        : base(new ConnectedClientId("unknown", -1), _ => { })
    {
    }

    public void OnClientUpdated()
    {
    }

    public void SetOnClientUpdatedAction(Action onClientUpdated)
    {
    }
}