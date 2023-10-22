using System;
using System.Collections.Generic;
using Core.Network.Shared.Contracts;

namespace TournamentServer.Server;

public class ConnectedClientInfoComparer : ConnectedClientComparer, IEqualityComparer<ConnectedClientInfo>
{
    private readonly ConnectedClientComparer _comparer = new();

    public bool Equals(ConnectedClientInfo? x, ConnectedClientInfo? y) => _comparer.Equals(x, y);

    public int GetHashCode(ConnectedClientInfo connectedClientInfo) => 
        _comparer.GetHashCode(connectedClientInfo);
}