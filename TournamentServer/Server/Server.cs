using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Core.Network;
using Core.Network.ExternalShared;
using Core.Network.ExternalShared.Contracts;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class Server :IServer
{
    public MonitoredVariable<bool> IsServerStarted { get; } = false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientConnected { get; } = false;
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
        _networkServer.CreateServer();
    }

    private void OnClientUpdated(ConnectedClientId connectedClientId)
    {
        Task.Run(() => ((ConnectedClientInfoArray)ClientsConnectedInfoArray).OnClientUpdated(connectedClientId));
    }

    private void OnClientConnected()
    {
        if (_networkServer == null)
        {
            throw new InvalidConstraintException($"{nameof(Server)}.{nameof(_networkServer)} should be created at this stage");
        }
        
        ClientsConnectedCount.SetVariable(_networkServer.ConnectedClientsCount.ToString());
        ClientsConnectedInfoArray.SetVariable(
            _networkServer.ConnectedClients.Select(x => new ConnectedClientInfo(x))
                .Cast<IConnectedClientInfo>().ToArray());
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
}