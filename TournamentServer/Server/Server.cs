using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Core.Network;
using Core.Network.Shared;
using Core.Network.Shared.Contracts;
using Core.Network.Shared.Enums;
using Core.Network.Shared.Interfaces;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class Server :IServer
{
    public MonitoredVariable<bool> IsServerStarted { get; } = false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientsReadyForTournament { get; } = false;
    public MonitoredVariable<string> ServerAddress { get; } = "unknown";
    public MonitoredVariable<string> ServerPort { get; } = "unknown";
    public MonitoredVariable<string> ClientsConnectedCount { get; } = "unknown";
    public MonitoredVariable<ConnectedClientInfoArray> ClientsConnectedInfoArray { get; } = 
        (ConnectedClientInfoArray)Array.Empty<IConnectedClientInfo>();
    public MonitoredVariable<string> ServerLogFile { get; } = "unknown";

    private INetworkServerObject? _networkServer = null;

    public void StartServer()
    {
        Task.Run(StartServerInternal);
    }

    private void StartServerInternal()
    {
        IsServerProcessingCommand.SetVariable(true);
        _networkServer = NetworkFactory.CreateNetworkObject<INetworkServerObject>(
            NetworkObjectType.Server, OnServerCreated, OnServerDestroyed);
        _networkServer.SetOnClientConnectedAction(OnClientConnected);
        _networkServer.SetOnClientUpdatedAction(OnClientUpdated);
        _networkServer.SetOnClientDisconnectedAction(OnClientDisconnected);
        _networkServer.CreateServer();
    }

    private void OnClientDisconnected(ConnectedClientId disconnectedClientId)
    {
        UpdateConnectedClients();

        if (_networkServer?.ConnectedClients.Length < 4 && !(_networkServer?.IsLocatorServiceStarted ?? true))
        {
            _networkServer.StartLocatorService();
        }
    }

    private void UpdateConnectedClients()
    {
        if (_networkServer == null)
        {
            throw new InvalidConstraintException(
                $"{nameof(Server)}.{nameof(_networkServer)} should be created at this stage");
        }

        ClientsConnectedCount.SetVariable(_networkServer.ConnectedClientsCount.ToString());
        ClientsConnectedInfoArray.SetVariable(
            _networkServer.ConnectedClients.Select(x => new ConnectedClientInfo(x))
                .Cast<IConnectedClientInfo>().ToArray());

        var clientsConnectedInfoArray = (ConnectedClientInfoArray)ClientsConnectedInfoArray;
        if (ClientsConnectedCount == "4")
        {
            clientsConnectedInfoArray.ForEach(x => 
                    x.IsReadyForTournamentStart.OnChanged.AddAction(Client_IsReadyForTournamentStartUpdated));
        }
        else
        {
            clientsConnectedInfoArray.ForEach(x => 
                    x.IsReadyForTournamentStart.OnChanged.RemoveAction(Client_IsReadyForTournamentStartUpdated));
        }
        
        Client_IsReadyForTournamentStartUpdated();
    }

    private void Client_IsReadyForTournamentStartUpdated()
    {
        if (ClientsConnectedCount == "4")
        {
            var clientsConnectedInfoArray = (ConnectedClientInfoArray)ClientsConnectedInfoArray;
            var isAllClientsReadyForTournamentStart = clientsConnectedInfoArray
                .All(x => x.IsReadyForTournamentStart);
            var isAllClientsReadyForGame = clientsConnectedInfoArray
                .All(x => x.Game == "Bomberman");
            
            IsClientsReadyForTournament.SetVariable(
                isAllClientsReadyForTournamentStart && isAllClientsReadyForGame);
        }
        else
        {
            IsClientsReadyForTournament.SetVariable(false);
        }

    }

    private void OnClientUpdated(ConnectedClientId connectedClientId)
    {
        Task.Run(() => ((ConnectedClientInfoArray)ClientsConnectedInfoArray).OnClientUpdated(connectedClientId));
    }

    private void OnClientConnected()
    {
        UpdateConnectedClients();
        
        if (_networkServer?.ConnectedClients.Length == 4 && (_networkServer?.IsLocatorServiceStarted ?? false))
        {
            _networkServer.StopLocatorService();
        }
    }

    private void OnServerDestroyed()
    {
        IsServerStarted.SetVariable(false);        
        IsServerProcessingCommand.SetVariable(false);
        ServerAddress.SetVariable("unknown");
        ServerPort.SetVariable("unknown");
        ClientsConnectedCount.SetVariable("unknown");
        ServerLogFile.SetVariable("unknown");
        _networkServer = null;
    }

    private void OnServerCreated()
    {
        if (_networkServer == null)
        {
            throw new InvalidConstraintException($"{nameof(Server)}.{nameof(_networkServer)} should be created at this stage");
        }
        
        IsServerStarted.SetVariable(true);       
        ServerAddress.SetVariable(_networkServer.ServerIP);
        ServerPort.SetVariable(_networkServer.ServerPort.ToString());
        ClientsConnectedCount.SetVariable(_networkServer.ConnectedClientsCount.ToString());
        IsServerProcessingCommand.SetVariable(false);
        ServerLogFile.SetVariable(NetworkSettings.ServerLogsFile);
    }
    
    public void StopServer()
    {
        Task.Run(StopServerInternal);
    }

    private void StopServerInternal()
    {
        ClientsConnectedInfoArray.SetVariable(Array.Empty<IConnectedClientInfo>());
        IsServerProcessingCommand.SetVariable(true);
        _networkServer?.DestroyServer();
        IsServerStarted.SetVariable(false);
    }

    private void StartTournamentInternal()
    {
        
    }
    
    public void StartTournament()
    {
        Task.Run(StartServerInternal);
    }
}