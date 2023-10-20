using System.Text.Json;
using Core.Network.Shared.Contracts.Messages;

namespace Core.Network.Shared.Contracts.Extensions;

public static class BaseMessageExtension
{
    public static string Serialize<T>(this T message) where T:BaseMessage 
        => JsonSerializer.Serialize(message);
    public static T? Deserialize<T>(this string serializedMessage) where T:BaseMessage 
        => JsonSerializer.Deserialize<T>(serializedMessage);
}