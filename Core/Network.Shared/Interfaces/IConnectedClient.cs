using Core.Network.Shared.Contracts.Messages;

namespace Core.Network.Shared.Interfaces;

public interface IConnectedClient : IEquatable<IConnectedClient>
{
    IConnectedClientId ConnectedClientId { get; }
    Action<BaseMessage, string>? OnMessageReceivedAction { get; }
    void SendMessage<T>(T messageObject) where T : BaseMessage;
    void SetOnMessageReceivedAction(Action<BaseMessage, string>? onMessageReceived);
}