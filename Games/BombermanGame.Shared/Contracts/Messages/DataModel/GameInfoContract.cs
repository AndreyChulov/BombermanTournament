using System.Text.Json.Serialization;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.Contracts.Messages.DataModel;

public class GameInfoContract : IGameInfo
{
    public FieldItemEnum[][] Field { get; set; }
    public int FieldHeight { get; set; }
    public int FieldWidth { get; set; }
    public PlayerInfoContract[] PlayersInfosContract { get; set; }
    [JsonIgnore]
    public IPlayerInfo[] PlayersInfos => PlayersInfosContract.Cast<IPlayerInfo>().ToArray();
    

    public static GameInfoContract Initialize(IGameInfo gameInfo)
    {
        return new()
        {
            FieldHeight = gameInfo.FieldHeight,
            Field = gameInfo.Field,
            FieldWidth = gameInfo.FieldWidth,
            PlayersInfosContract = 
                gameInfo.PlayersInfos
                    .Select(PlayerInfoContract.Initialize)
                    .ToArray()
        };
    }
}