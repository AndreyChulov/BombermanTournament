using System.Runtime.Serialization;
using System.Text.Json;
using Core.Network;
using Core.Network.InternalShared;
using Core.Network.Shared;
using Core.Network.Shared.Contracts.Messages;
using Core.Network.Shared.Enums;
using Core.Network.Shared.Interfaces;
using Games.BombermanGame.Shared.Contracts.Messages;

namespace BombermanClient_C_Sharp;

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
                    ClientInfoMessage.Initialize(
                        _bot.Nickname, _bot.StrategyDescription, 
                        _bot.IsDebugMode, _bot.AiDevelopedForGame);
                _networkClient.SendMessage(responseMessage);
                baseResponseMessage = responseMessage;
                break;
            case TurnInfoMessage.MessageString:
                var turnInfoMessage = JsonSerializer.Deserialize<TurnInfoMessage>(serializedMessage);

                if (turnInfoMessage == null)
                {
                    throw new SerializationException($"Can not deserialize [{nameof(TurnInfoMessage)}]");
                }

                var botTurn = _bot.Turn(turnInfoMessage.GameInfo, turnInfoMessage.CurrentPlayerInfo);
                var botCommandMessage = BotCommandMessage.Initialize(botTurn);
                _networkClient.SendMessage(botCommandMessage);
                baseResponseMessage = botCommandMessage;
                
                break;
            case TurnTimeoutExceededMessage.MessageString:
                _bot.OnTurnTimeExceeded();
                break;
            default:
                throw new ArgumentException("Unknown message received from network");
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
    }
}