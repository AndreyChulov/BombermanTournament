using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace BombermanGame.Game.DataModel
{
    public class GameInfo : IGameInfo
    {
        public FieldItemEnum[][] Field { get; }
        public int FieldHeight { get; }
        public int FieldWidth { get; }
        public IPlayerInfo[] PlayersInfos { get; }

        public GameInfo(FieldItemEnum[][] field, IPlayerInfo[] playersInfos)
        {
            Field = field;
            PlayersInfos = playersInfos;
            FieldHeight = field.Length;
            FieldWidth = field[0].Length;
        }
    }
}