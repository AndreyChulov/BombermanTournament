using System.ComponentModel.Design.Serialization;
using Core.Network.Shared.Contracts;
using Core.Network.Shared.Contracts.Messages;
using TournamentServer.Shared.Utilities;

namespace TournamentServer.Shared;

public class ConnectedClientInfoStub : ConnectedClient, IConnectedClientInfo
{
    public MonitoredVariable<bool> IsReadyForTournamentStart => false;
    public MonitoredVariable<bool> IsDebugMode => false;
    public MonitoredVariable<string> NickName => "unknown";
    public MonitoredVariable<string> StrategyDescription => "unknown";
    public MonitoredVariable<string> Game => "unknown";
    public new Action<BaseMessage, string>? OnMessageReceivedAction { get; set; }

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