using BombermanGame.Shared.Enums;

namespace BombermanGame.Shared.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// Short name for AI player
        /// </summary>
        string Nickname { get; }
        /// <summary>
        /// AI strategy description for tournament information
        /// </summary>
        string StrategyDescription { get; }

        bool IsDebugMode { get; }

        /// <summary>
        /// Method which is used for turn calculation
        /// </summary>
        /// <param name="gameInfo">Whole game information that can be required</param>
        /// <param name="currentPlayerInfo">Info about current player</param>
        /// <returns>Enum that describe current player turn request</returns>
        PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo);

        void OnTurnTimeExceeded();
    }
}