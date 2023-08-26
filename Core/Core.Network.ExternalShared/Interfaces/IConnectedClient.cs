namespace Core.Network.ExternalShared.Interfaces;

public interface IConnectedClient : IEquatable<IConnectedClient>
{
    IConnectedClientId ConnectedClientId { get; }
}