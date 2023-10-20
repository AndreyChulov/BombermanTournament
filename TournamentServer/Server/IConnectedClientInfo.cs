using System;
using Core.Network.Shared.Interfaces;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public interface IConnectedClientInfo : IConnectedClient
{
    MonitoredVariable<bool> IsReadyForTournamentStart { get; }
    MonitoredVariable<string> NickName { get; } 
    MonitoredVariable<string> StrategyDescription { get; }
    void OnClientUpdated();
    void SetOnClientUpdatedAction(Action onClientUpdated);
}