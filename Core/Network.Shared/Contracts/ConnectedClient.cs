using Core.Network.Shared.Contracts.Messages;
using Core.Network.Shared.Interfaces;

namespace Core.Network.Shared.Contracts;

public class ConnectedClient : IConnectedClient
{
    public IConnectedClientId ConnectedClientId { get; }
    private readonly Action<BaseMessage> _sendMessageAction;
    
    public Action<BaseMessage, string>? OnMessageReceivedAction { get; private set; }

    public ConnectedClient(IConnectedClientId connectedClientId, Action<BaseMessage> sendMessageAction)
    {
        _sendMessageAction = sendMessageAction;
        
        OnMessageReceivedAction = null;

        ConnectedClientId = connectedClientId;
    }

    public bool Equals(IConnectedClient? other)
    {
        return ConnectedClientId.Equals(other?.ConnectedClientId);
    }

    public void SendMessage<T>(T messageObject)
        where T:BaseMessage
    {
        Task.Run(() => _sendMessageAction(messageObject));
    }

    public void RiseMessageReceived(BaseMessage baseMessage, string serializedMessage)
    {
        if (OnMessageReceivedAction == null)
        {
            return;
        }

        Task.Run(() => OnMessageReceivedAction(baseMessage, serializedMessage));
    }

    public void SetOnMessageReceivedAction(Action<BaseMessage, string>? onMessageReceived)
    {
        OnMessageReceivedAction = onMessageReceived;
    }
}