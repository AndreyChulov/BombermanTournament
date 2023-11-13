using System.Text.Json.Serialization;
using Games.BombermanGame.Shared.Enums;
using Games.BombermanGame.Shared.Interfaces;

namespace Games.BombermanGame.Shared.Contracts.Messages.DataModel;

public class GameInfoContract : IGameInfo
{
    [JsonIgnore]
    public FieldItemEnum[][] Field { get; set; }
    public int FieldHeight { get; set; }
    public int FieldWidth { get; set; }
    [JsonIgnore]
    public IPlayerInfo[] PlayersInfos { get; set; }

    public static GameInfoContract Initialize(IGameInfo gameInfo)
    {
        return new()
        {
            FieldHeight = gameInfo.FieldHeight,
            Field = gameInfo.Field,
            FieldWidth = gameInfo.FieldWidth,
            PlayersInfos = gameInfo.PlayersInfos
        };
    }
}