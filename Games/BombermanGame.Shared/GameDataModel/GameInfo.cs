using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel
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

        public GameInfo(Field field, IPlayerInfo[] playersInfos)
        {
            Field = field.GetFieldCloned();
            PlayersInfos = playersInfos;
            FieldHeight = field.FieldHeight;
            FieldWidth = field.FieldWidth;
        }

        public static GameInfo Create(Field field, IPlayerInfo[] playersInfos) 
            => new GameInfo(field, playersInfos);
    }
}