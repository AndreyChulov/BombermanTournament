using Games.BombermanGame.Shared.GameDataModel;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Game
{
    [Obsolete]
    public abstract class BombermanGameThread
    {
        private readonly int _playerTurnMillisecondsTimeout;
        protected PlayerCollection Players { get; }

        private List<Task> _playersTurnTasks;
        private Thread _gameThread;
        private bool _isGameThreadStarted;
        private object _isGameThreadStartedLockObject = new();

        protected BombermanGameThread(PlayerCollection players, int playerTurnMillisecondsTimeout)
        {
            _playerTurnMillisecondsTimeout = playerTurnMillisecondsTimeout;
            Players = players;
            _gameThread = new Thread(GameThreadFunction);
            _isGameThreadStarted = false;
            _playersTurnTasks = new List<Task>
            {
                Task.CompletedTask,
                Task.CompletedTask,
                Task.CompletedTask,
                Task.CompletedTask,
            };
        }

        public void Start()
        {
            _gameThread.Start();
        }

        public void Stop()
        {
            lock (_isGameThreadStartedLockObject)
            {
                _isGameThreadStarted = false;
            }
        }
        
        private void GameThreadFunction()
        {
            lock (_isGameThreadStartedLockObject)
            {
                _isGameThreadStarted = true;
            }

            while (_isGameThreadStarted)
            {
                ExecutePlayerTurn(Players.Player1, 0, _playerTurnMillisecondsTimeout);
                ExecutePlayerTurn(Players.Player2, 1, _playerTurnMillisecondsTimeout);
                ExecutePlayerTurn(Players.Player3, 2, _playerTurnMillisecondsTimeout);
                ExecutePlayerTurn(Players.Player4, 3, _playerTurnMillisecondsTimeout);
                OnTurnFinished();
            }
        }

        protected abstract void OnTurnFinished();

        private void ExecutePlayerTurn(IPlayer currentPlayer, int taskIndex, int millisecondsTimeout)
        {
            if (_playersTurnTasks[taskIndex].IsCompleted)
            {
                _playersTurnTasks[taskIndex] = Task.Run(() => TimeLimitedPlayerTurn(currentPlayer, taskIndex));
            }

            if (!_playersTurnTasks[taskIndex].Wait(millisecondsTimeout))
            {
                currentPlayer.OnTurnTimeExceeded();
            }

            Thread.CurrentThread.Join(300);
        }

        protected abstract void TimeLimitedPlayerTurn(IPlayer currentPlayerTurn, int currentPlayerIndex);
    }
}