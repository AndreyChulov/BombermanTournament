using Core.Engine.Shared.Interfaces;
using Core.EngineFactory;
using Games.BombermanGame.NetworkGame;
using Games.BombermanGame.NetworkGame.DrawDataModel;
using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.GameDataModel;

namespace Games.BombermanGame;

public partial class BombermanNetworkGameForm : Form
{
    private IEngine? _engine = null;
    private BombermanNetworkGame _game;
    
    public BombermanNetworkGameForm(BombermanNetworkGame game)
    {
        _game = game;
        InitializeComponent();
    }

    private void BombermanNetworkGameForm_Load(object? sender, EventArgs e)
    {
        //var playerCollection = new PlayerCollection(_player1, _player2, _player3, _player4);
        //_game = new(playerCollection, 
        //    _player1.IsDebugMode || _player2.IsDebugMode || _player3.IsDebugMode || _player4.IsDebugMode);
            
        _engine = EngineFactory.CreateEngine(this, 2f);
        _engine.LoadDrawObject(Background.Create(_engine));
        _engine.LoadDrawObject(FpsMeter.Create(_engine));
        _engine.LoadDrawObject(NetworkGame.DrawDataModel.Field.Create(_engine, _game));
        _engine.LoadDrawObject(Scores.Create(_engine, _game));
        //_engine.LoadDrawObject(Player1Score.Create(_engine, _game.PlayerInfoCollection.Player1Info));
        //_engine.LoadDrawObject(Player2Score.Create(_engine, _game.PlayerInfoCollection.Player2Info));
        //_engine.LoadDrawObject(Player3Score.Create(_engine, _game.PlayerInfoCollection.Player3Info));
        //_engine.LoadDrawObject(Player4Score.Create(_engine, _game.PlayerInfoCollection.Player4Info));
        _engine.LoadResources();
        _engine.Start();
        //_game.Start();

        /*var gameTimerTask = Task.Delay(TimeSpan.FromMinutes(3));
        gameTimerTask
            .ContinueWith(_ => _game.Stop())
            .ContinueWith(
                _ => MessageBox.Show(
                    "Game stopped!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning
                )
            );*/
        //throw new NotImplementedException();
    }

    private void BombermanNetworkGameForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        //_game?.Stop();
        _engine?.Stop();
        _engine?.Dispose();
        //throw new NotImplementedException();
    }
}