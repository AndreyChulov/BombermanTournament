using Core.Engine.Shared.Interfaces;
using Core.EngineFactory;
using Games.BombermanGame.Game;
using Games.BombermanGame.ObsoleteGame.DrawDataModel.Draw.Score;
using Games.BombermanGame.Shared.DrawDataModel;
using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.GameDataModel.Player;
using Games.BombermanGame.Shared.Interfaces;
using Field = Games.BombermanGame.ObsoleteGame.DrawDataModel.Draw.Field.Field;

namespace Games.BombermanGame
{

    [Obsolete]
    public partial class BombermanGameForm : Form
    {
        private readonly IPlayer _player1;
        private readonly IPlayer _player2;
        private readonly IPlayer _player3;
        private readonly IPlayer _player4;
        private IEngine? _engine = null;
        private global::Games.BombermanGame.Game.BombermanGame? _game;
        
        public BombermanGameForm(
            IPlayer? player1, IPlayer? player2, IPlayer? player3, IPlayer? player4)
        {
            _player1 = player1 ?? new DefaultBombermanPlayer();
            _player2 = player2 ?? new DefaultBombermanPlayer();
            _player3 = player3 ?? new DefaultBombermanPlayer();
            _player4 = player4 ?? new DefaultBombermanPlayer();
            InitializeComponent();
        }

        private void BombermanGameForm_Load(object sender, EventArgs e)
        {
            var playerCollection = new PlayerCollection(_player1, _player2, _player3, _player4);
            _game = new(playerCollection, 
                _player1.IsDebugMode || _player2.IsDebugMode || _player3.IsDebugMode || _player4.IsDebugMode);
            
            _engine = EngineFactory.CreateEngine(this, 2f);
            _engine.LoadDrawObject(Background.Create(_engine));
            _engine.LoadDrawObject(FpsMeter.Create(_engine));
            _engine.LoadDrawObject(Field.Create(_engine, _game));
            _engine.LoadDrawObject(Player1Score.Create(_engine, _game.PlayerInfoCollection.Player1Info));
            _engine.LoadDrawObject(Player2Score.Create(_engine, _game.PlayerInfoCollection.Player2Info));
            _engine.LoadDrawObject(Player3Score.Create(_engine, _game.PlayerInfoCollection.Player3Info));
            _engine.LoadDrawObject(Player4Score.Create(_engine, _game.PlayerInfoCollection.Player4Info));
            _engine.LoadResources();
            _engine.Start();
            _game.Start();

            var gameTimerTask = Task.Delay(TimeSpan.FromMinutes(3));
            gameTimerTask
                .ContinueWith(_ => _game.Stop())
                .ContinueWith(
                    _ => MessageBox.Show(
                        "Game stopped!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning
                    )
                );
        }

        private void BombermanGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _game?.Stop();
            _engine?.Stop();
            _engine?.Dispose();
        }
    }
}