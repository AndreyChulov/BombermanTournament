using BombermanGame.Game.DataModel;
using BombermanGame.Game.Extensions;
using BombermanGame.Shared.Enums;
using BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game
{
    public class BombermanGame : BombermanGameThread
    {
        //private PlayerCollection _playerCollection;
        private Field _field;
        public PlayerInfoCollection PlayerInfoCollection { get; }
        private List<Bomb> _bombs;

        public FieldItemEnum[][] GetFieldEnum() => _field.GetField();
        public FieldItemEnum[][] GetFieldEnumForPlayers() => 
            _field
                .GetFieldCloned()
                .Select(
                    fieldLine=>fieldLine.Select(RemovePlayerStartPoints).ToArray()
                ).ToArray();

        private static FieldItemEnum RemovePlayerStartPoints(FieldItemEnum fieldItem)
        {
            return IsPlayerStartPoint(fieldItem) ? FieldItemEnum.EmptyField : fieldItem;
        }

        private static bool IsPlayerStartPoint(FieldItemEnum fieldItem)
        {
            return fieldItem == FieldItemEnum.Player1StartPoint ||
                   fieldItem == FieldItemEnum.Player2StartPoint ||
                   fieldItem == FieldItemEnum.Player3StartPoint ||
                   fieldItem == FieldItemEnum.Player4StartPoint;
        }


        public BombermanGame(PlayerCollection playerCollection, bool isDebugMode) 
            : base(MixPlayers(playerCollection), isDebugMode ? -1 : 300)
        {
            _bombs = new List<Bomb>();
            _field = InitializeField();
            PlayerInfoCollection = new PlayerInfoCollection(
                new PlayerInfo(Players.Player1.Nickname, _field.GetPlayerStartPosition(0)),
                new PlayerInfo(Players.Player2.Nickname, _field.GetPlayerStartPosition(1)),
                new PlayerInfo(Players.Player3.Nickname, _field.GetPlayerStartPosition(2)),
                new PlayerInfo(Players.Player4.Nickname, _field.GetPlayerStartPosition(3))
            );
        }

        private static PlayerCollection MixPlayers(PlayerCollection playerCollection)
        {
            Random randomGenerator = new();
            
            List<KeyValuePair<IPlayer, int>> playerLottery = new List<KeyValuePair<IPlayer, int>>
            {
                new(playerCollection.Player1, randomGenerator.Next(100)),
                new(playerCollection.Player2, randomGenerator.Next(100)),
                new(playerCollection.Player3, randomGenerator.Next(100)),
                new(playerCollection.Player4, randomGenerator.Next(100)),
            };
            
            var sortedPlayersLottery = playerLottery.OrderBy(x=>x.Value).ToArray();
            
            return new PlayerCollection(
                sortedPlayersLottery[0].Key, sortedPlayersLottery[1].Key,
                sortedPlayersLottery[2].Key, sortedPlayersLottery[3].Key);
        }

        private Field InitializeField()
        {
            int fieldWidth = 17;
            int fieldHeight = 17;
            Field field = new(fieldWidth,fieldHeight);
            var randomizer = new Random();

            for (int rowCounter = 0; rowCounter < fieldHeight; rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < fieldWidth; columnCounter++)
                {
                    if (SetSandboxIndestructibleField(rowCounter, columnCounter, field))
                    {
                        continue;
                    }

                    if (SetSanboxEmptyField(rowCounter, columnCounter, fieldHeight, fieldWidth, field))
                    {
                        continue;
                    }

                    SetDestructibleField(randomizer, field, rowCounter, columnCounter, 
                        10);
                }
            }
            
            SetPlayersStartPoints(field, fieldHeight, fieldWidth);

            //field.SetFieldCell(0, 1, FieldItemEnum.Bomb);

            return field;
        }

        private static void SetPlayersStartPoints(Field field, int fieldHeight, int fieldWidth)
        {
            field.SetFieldCell(0, 0,
                FieldItemEnum.Player1StartPoint);
            field.SetFieldCell(fieldHeight - 1, 0,
                FieldItemEnum.Player2StartPoint);
            field.SetFieldCell(fieldHeight - 1, fieldWidth - 1,
                FieldItemEnum.Player3StartPoint);
            field.SetFieldCell(0, fieldWidth - 1,
                FieldItemEnum.Player4StartPoint);
        }

        private static void SetDestructibleField(
            Random randomizer, Field field, int rowCounter, int columnCounter, 
            int destructibleFieldPercentage)
        {
            if (randomizer.Next(100) < destructibleFieldPercentage)
            {
                field.SetFieldCell(rowCounter, columnCounter, FieldItemEnum.DestructibleField);
                return;
            }
        }

        private static bool SetSanboxEmptyField(int rowCounter, int columnCounter, int fieldHeight, int fieldWidth, Field field)
        {
            if (
                (rowCounter < 2 && columnCounter < 2) ||
                (rowCounter > fieldHeight - 3 && columnCounter < 2) ||
                (rowCounter > fieldHeight - 3 && columnCounter > fieldWidth - 3) ||
                (rowCounter < 2 && columnCounter > fieldWidth - 3)
            )
            {
                field.SetFieldCell(rowCounter, columnCounter, FieldItemEnum.EmptyField);
                return true;
            }

            return false;
        }

        private static bool SetSandboxIndestructibleField(int rowCounter, int columnCounter, Field field)
        {
            if (rowCounter % 2 == 1 && columnCounter % 2 == 1)
            {
                field.SetFieldCell(rowCounter, columnCounter, FieldItemEnum.IndestructibleField);
                return true;
            }

            return false;
        }

        protected override void TimeLimitedPlayerTurn(IPlayer currentPlayerTurn, int currentPlayerIndex)
        {
            var playerInfos = PlayerInfoCollection.GetPlayerInfos();
            var gameInfo = new GameInfo(GetFieldEnumForPlayers(), playerInfos);
            var currentPlayerInfo = (PlayerInfo) playerInfos[currentPlayerIndex];
            var playerTurn = currentPlayerTurn.Turn(gameInfo, currentPlayerInfo);

            var currentFieldItem = _field.GetCurrentFieldItem(currentPlayerInfo);

            switch (playerTurn)
            {
                case PlayerTurnEnum.MoveDown:
                    if (_field.GetDownFieldItem(currentPlayerInfo) == FieldItemEnum.EmptyField)
                    {
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemovePlayerFromFieldItem());
                        currentPlayerInfo.MoveDown();
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemoveBombFromFieldItem());
                    }
                    break;
                
                case PlayerTurnEnum.MoveUp:
                    if (_field.GetUpFieldItem(currentPlayerInfo) == FieldItemEnum.EmptyField)
                    {
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemovePlayerFromFieldItem());
                        currentPlayerInfo.MoveUp();
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemoveBombFromFieldItem());
                    }
                    break;
                
                case PlayerTurnEnum.MoveLeft:
                    if (_field.GetLeftFieldItem(currentPlayerInfo) == FieldItemEnum.EmptyField)
                    {
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemovePlayerFromFieldItem());
                        currentPlayerInfo.MoveLeft();
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemoveBombFromFieldItem());
                    }
                    break;
                
                case PlayerTurnEnum.MoveRight:
                    if (_field.GetRightFieldItem(currentPlayerInfo) == FieldItemEnum.EmptyField)
                    {
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemovePlayerFromFieldItem());
                        currentPlayerInfo.MoveRight();
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.RemoveBombFromFieldItem());
                    }
                    break;
                
                case PlayerTurnEnum.PutBomb:
                    if (!currentFieldItem.IsBombOnField())
                    {
                        _field.SetFieldCell(currentPlayerInfo, currentFieldItem.AddBombToFieldItem());
                        _bombs.Add(new Bomb(currentPlayerInfo));
                    }
                    break;
            }

        }

        protected override void OnTurnFinished()
        {
            _bombs.ForEach(x=>x.Tick());
            _bombs.Where(x=>x.TicksToBoom == 0).ToList().ForEach(BlowBomb);
            _bombs = _bombs.Where(x => x.TicksToBoom > 0).ToList();
        }

        private void BlowBomb(Bomb bomb)
        {
            var bombFieldItem = _field.GetCurrentFieldItem(bomb);
            
            _field.SetFieldCell(bomb, bombFieldItem.RemoveBombFromFieldItem());
            
            var bombUpFieldItem = _field.GetUpFieldItem(bomb);
            var bombDownFieldItem = _field.GetDownFieldItem(bomb);
            var bombLeftFieldItem = _field.GetLeftFieldItem(bomb);
            var bombRightFieldItem = _field.GetRightFieldItem(bomb);

            UpdateScoreByBlowingUpBomb(bomb, bombFieldItem);
            UpdateScoreByBlowingUpBomb(bomb, bombUpFieldItem);
            UpdateScoreByBlowingUpBomb(bomb, bombDownFieldItem);
            UpdateScoreByBlowingUpBomb(bomb, bombLeftFieldItem);
            UpdateScoreByBlowingUpBomb(bomb, bombRightFieldItem);

            if (bombUpFieldItem == FieldItemEnum.DestructibleField)
            {
                _field.SetFieldCell(bomb.Y - 1, bomb.X, FieldItemEnum.EmptyField);
            }

            if (bombDownFieldItem == FieldItemEnum.DestructibleField)
            {
                _field.SetFieldCell(bomb.Y + 1, bomb.X, FieldItemEnum.EmptyField);
            }
            
            if (bombLeftFieldItem == FieldItemEnum.DestructibleField)
            {
                _field.SetFieldCell(bomb.Y, bomb.X - 1, FieldItemEnum.EmptyField);
            }
            
            if (bombRightFieldItem == FieldItemEnum.DestructibleField)
            {
                _field.SetFieldCell(bomb.Y, bomb.X + 1, FieldItemEnum.EmptyField);
            }
        }

        private void UpdateScoreByBlowingUpBomb(Bomb bomb, FieldItemEnum? bombFieldItem)
        {
            switch (bombFieldItem)
            {
                case FieldItemEnum.Player1:
                case FieldItemEnum.Player1WithBomb:
                    PlayerInfoCollection.Player1Info.BlowUpPlayer();
                    break;
                case FieldItemEnum.Player2:
                case FieldItemEnum.Player2WithBomb:
                    PlayerInfoCollection.Player2Info.BlowUpPlayer();
                    break;
                case FieldItemEnum.Player3:
                case FieldItemEnum.Player3WithBomb:
                    PlayerInfoCollection.Player3Info.BlowUpPlayer();
                    break;
                case FieldItemEnum.Player4:
                case FieldItemEnum.Player4WithBomb:
                    PlayerInfoCollection.Player4Info.BlowUpPlayer();
                    break;
                case FieldItemEnum.DestructibleField:
                    bomb.Owner.PlayerBlowUpDestroyableCell();
                    break;
            }
        }
    }
}