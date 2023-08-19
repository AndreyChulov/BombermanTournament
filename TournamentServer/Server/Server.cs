using Core.Network;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using TournamentServer.Server.Utilities;

namespace TournamentServer.Server;

public class Server :IServer
{
    public MonitoredVariable<bool> IsServerStarted { get; } = false;
    public MonitoredVariable<bool> IsServerProcessingCommand { get; } = false;
    public MonitoredVariable<bool> IsClientConnected { get; } = false;

    private INetworkServerObject? _networkServerObject = null;
    
    public void StartServer()
    {
        IsServerProcessingCommand.SetVariable(true);
        _networkServerObject = NetworkFactory.CreateNetworkObject<INetworkServerObject>(
            NetworkObjectType.Server, OnServerCreated, OnServerDestroyed);
        _networkServerObject.StartServer();
    }

    private void OnServerDestroyed()
    {
        IsServerStarted.SetVariable(false);        
        IsServerProcessingCommand.SetVariable(false);
        _networkServerObject = null;
    }

    private void OnServerCreated()
    {
        IsServerStarted.SetVariable(true);        
        IsServerProcessingCommand.SetVariable(false);
    }

    public void StopServer()
    {
        _networkServerObject?.StopServer();
        IsServerStarted.SetVariable(false);
        IsServerProcessingCommand.SetVariable(true);
    }
}