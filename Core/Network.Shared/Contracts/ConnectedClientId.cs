using Core.Network.Shared.Interfaces;

namespace Core.Network.Shared.Contracts;

public class ConnectedClientId : IEquatable<ConnectedClientId>, IConnectedClientId
{
    public string ClientIP { get; }
    public int ClientPort { get; }

    public bool Equals(ConnectedClientId? other)
    {
        if (other == null)
        {
            return false;
        }
        
        return ClientIP == other.ClientIP && ClientPort == other.ClientPort;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        
        return Equals((ConnectedClientId)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClientIP, ClientPort);
    }

    public ConnectedClientId(string clientIp, int clientPort)
    {
        ClientIP = clientIp;
        ClientPort = clientPort;
    }
    
    public ConnectedClientId(IConnectedClientId source) : this(source.ClientIP, source.ClientPort) { }
}