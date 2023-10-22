using Core.Network.Shared.Contracts.Messages;
using Core.Network.Shared.Interfaces;
using TournamentServer.Shared.Utilities;

namespace TournamentServer.Shared;

public interface IConnectedClientInfo : IConnectedClient
{
    MonitoredVariable<bool> IsReadyForTournamentStart { get; }
    MonitoredVariable<bool> IsDebugMode { get; }
    MonitoredVariable<string> NickName { get; } 
    MonitoredVariable<string> StrategyDescription { get; }
    MonitoredVariable<string> Game { get; }
    new Action<BaseMessage, string>? OnMessageReceivedAction { set; }
    void OnClientUpdated();
    void SetOnClientUpdatedAction(Action onClientUpdated);
    
}