using System.Text.Json;

namespace Core.Network.ExternalShared.Contracts.Messages;

public class BaseMessage
{
    public string Message { get; set; } = String.Empty;
}