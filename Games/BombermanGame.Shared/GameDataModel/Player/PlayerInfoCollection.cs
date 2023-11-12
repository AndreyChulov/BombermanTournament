using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.GameDataModel.Player
{
    public class PlayerInfoCollection
    {
        public IPlayerInfo Player1Info { get; }
        public IPlayerInfo Player2Info { get; }
        public IPlayerInfo Player3Info { get; }
        public IPlayerInfo Player4Info { get; }
        public IPlayerInfo[] PlayerInfos { get; }

        public PlayerInfoCollection(
            in IPlayerInfo player1Info, IPlayerInfo player2Info, IPlayerInfo player3Info, IPlayerInfo player4Info)
        {
            Player1Info = player1Info;
            Player2Info = player2Info;
            Player3Info = player3Info;
            Player4Info = player4Info;

            PlayerInfos = new[] {player1Info, player2Info, player3Info, player4Info};
        }

        public IPlayerInfo GetPlayerInfo(int playerIndex)
        {
            return playerIndex switch
            {
                0 => Player1Info,
                1 => Player2Info,
                2 => Player3Info,
                3 => Player4Info,
                _ => throw new InvalidOperationException(
                    $"Unexpected [{nameof(playerIndex)}] value [{playerIndex}]")
            };
        }
    }
}