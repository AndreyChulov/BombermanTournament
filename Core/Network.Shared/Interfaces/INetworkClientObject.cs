using Core.Network.Shared.Contracts.Messages;

namespace Core.Network.Shared.Interfaces;

public interface INetworkClientObject : INetworkObject
{
    void StartClient();
    void StopClient();
    void SetOnMessageReceivedAction(Action<BaseMessage, string> onMessageReceived);
    void SendMessage<T>(T messageObject) where T : BaseMessage;
}