using Core.Network;
using Core.Network.InternalShared;
using Core.Network.Shared;
using Core.Network.Shared.Contracts.Messages;
using Core.Network.Shared.Enums;
using Core.Network.Shared.Interfaces;

namespace Bots.Bomberman.DumbBot;

public class Client : IDisposable
{
    private readonly Bot _bot;
    private readonly INetworkClientObject _networkClient;

    public Client(Bot bot)
    {
        _bot = bot;
        
        Logger.Initialize(NetworkSettings.ClientLogsFiles);
        _networkClient = NetworkFactory.CreateNetworkObject<INetworkClientObject>(NetworkObjectType.Client,
            OnNetworkClientCreated, OnNetworkClientDestroyed);
        _networkClient.SetOnMessageReceivedAction(NetworkClient_OnMessageReceived);
        _networkClient.StartClient();
    }

    private void NetworkClient_OnMessageReceived(BaseMessage baseMessage, string serializedMessage)
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Message received: Message= {baseMessage.Message}");
        BaseMessage? baseResponseMessage = null;

        switch (baseMessage.Message)
        {
            case GetClientInfoMessage.MessageString:
                var responseMessage =
                    ClientInfoMessage.Initialize(_bot.Nickname, _bot.StrategyDescription, _bot.IsDebugMode);
                _networkClient.SendMessage(responseMessage);
                baseResponseMessage = responseMessage;
                break;
            default:
                throw new NotImplementedException();
        }

        if (baseResponseMessage != null)
        {
            Console.WriteLine(
                $"{DateTime.Now.ToLongTimeString()} Response sent: Message= {baseResponseMessage.Message}");
        }
    }

    private void OnNetworkClientDestroyed()
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Client object destroyed.");
    }

    private void OnNetworkClientCreated()
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Client object created.");
    }

    public void Dispose()
    {
        _networkClient.StopClient();
        Logger.FreeUpResources();
        // TODO release managed resources here
    }
}