using System.Threading.Tasks;
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
        Task.Run(() =>
        {
            IsServerProcessingCommand.SetVariable(true);
            _networkServerObject = NetworkFactory.CreateNetworkObject<INetworkServerObject>(
                NetworkObjectType.Server, OnServerCreated, OnServerDestroyed);
            _networkServerObject.CreateServer();
        });
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
        Task.Run(() =>
        {
            IsServerProcessingCommand.SetVariable(true);        
            _networkServerObject?.DestroyServer();
            IsServerStarted.SetVariable(false);
        });
    }
}