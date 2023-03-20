using BombermanGame.DrawDataModel.Draw;
using BombermanGame.DrawDataModel.Draw.Score;
using BombermanGame.Game;
using BombermanGame.Game.DataModel;
using BombermanGame.Shared.Interfaces;
using Engine;
using Engine.SharedInterfaces;
using Field = BombermanGame.DrawDataModel.Draw.Field.Field;

namespace BombermanGame
{
    public partial class BombermanGameForm : Form
    {
        private readonly IPlayer _player1;
        private readonly IPlayer _player2;
        private readonly IPlayer _player3;
        private readonly IPlayer _player4;
        private IEngine? _engine = null;
        private Game.BombermanGame? _game;
        
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
        }

        private void BombermanGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _game?.Stop();
            _engine?.Stop();
            _engine?.Dispose();
        }
    }
}