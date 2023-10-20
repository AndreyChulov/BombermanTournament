using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Interfaces
{
    /// <summary>
    /// Information about the game stage
    /// </summary>
    public interface IGameInfo
    {
        FieldItemEnum[][] Field { get; }
        int FieldHeight { get; }
        int FieldWidth { get; }
        
        IPlayerInfo[] PlayersInfos { get; }
    }
}