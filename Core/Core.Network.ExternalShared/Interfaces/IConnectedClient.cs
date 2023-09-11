using Core.Network.ExternalShared.Contracts.Messages;

namespace Core.Network.ExternalShared.Interfaces;

public interface IConnectedClient : IEquatable<IConnectedClient>
{
    IConnectedClientId ConnectedClientId { get; }
    Action<BaseMessage, string>? OnMessageReceivedAction { get; }
    void SendMessage<T>(T messageObject) where T : BaseMessage;
    void SetOnMessageReceivedAction(Action<BaseMessage, string>? onMessageReceived);
}