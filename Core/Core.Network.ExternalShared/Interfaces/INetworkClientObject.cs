using Core.Network.ExternalShared.Contracts.Messages;

namespace Core.Network.ExternalShared.Interfaces;

public interface INetworkClientObject : INetworkObject
{
    void StartClient();
    void StopClient();
    void SetOnMessageReceivedAction(Action<BaseMessage, string> onMessageReceived);
    void SendMessage<T>(T messageObject) where T : BaseMessage;
}