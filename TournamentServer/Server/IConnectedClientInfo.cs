using System;
using Core.Network.ExternalShared.Interfaces;

namespace TournamentServer.Server;

public interface IConnectedClientInfo : IEquatable<IConnectedClientInfo>, IConnectedClient
{
    
}