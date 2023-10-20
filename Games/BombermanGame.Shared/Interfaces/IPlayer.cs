using Games.BombermanGame.Shared.Enums;

namespace Games.BombermanGame.Shared.Interfaces
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

        /// <summary>
        /// Is bot in debug mode (disabled timeout for turn) 
        /// </summary>
        bool IsDebugMode { get; }
        
        /// <summary>
        /// The game name for which Ai/bot was developed
        /// </summary>
        string AiDevelopedForGame { get; }

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