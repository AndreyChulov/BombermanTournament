using System;
using Core.Network.ExternalShared.Interfaces;

namespace TournamentServer.Server;

public interface IConnectedClientInfo : IConnectedClient
{
    void OnClientUpdated();
    void SetOnClientUpdatedAction(Action onClientUpdated);
}