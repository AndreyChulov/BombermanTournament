using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Network.Shared.Contracts;
using Core.Network.Shared.Contracts.Messages;
using Core.Network.Shared.Interfaces;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class ConnectedClientInfo : ConnectedClient, IConnectedClientInfo
{
    private Action? _onClientUpdated;
    public MonitoredVariable<bool> IsReadyForTournamentStart { get; } = false;
    public MonitoredVariable<string> NickName { get; } = "unknown";
    public MonitoredVariable<string> StrategyDescription { get; } = "unknown";    
    public MonitoredVariable<string> Game { get; } = "unknown";

    public ConnectedClientInfo(IConnectedClient connectedClient) 
        : base(connectedClient.ConnectedClientId, connectedClient.SendMessage)
    {
        _onClientUpdated = null;
        
        connectedClient.SetOnMessageReceivedAction(ConnectedClient_OnMessageReceived);
        SendMessage(GetClientInfoMessage.Initialize());
    }

    private void ConnectedClient_OnMessageReceived(BaseMessage baseMessage, string serializedMessage)
    {
        switch (baseMessage.Message)
        {
            case ClientInfoMessage.MessageString:
                var clientInfoMessage = JsonSerializer.Deserialize<ClientInfoMessage>(serializedMessage) ??
                                        new ClientInfoMessage();
                NickName.SetVariable(clientInfoMessage.Nickname);
                StrategyDescription.SetVariable(clientInfoMessage.StrategyDescription);
                Game.SetVariable(clientInfoMessage.DevelopedForGame);
                IsReadyForTournamentStart.SetVariable(true);
                break;
            default:
                throw new NotImplementedException();
                break;
        }
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