namespace BombermanGame.Shared.Enums
{
    /// <summary>
    /// Enum that describe current player turn request
    /// </summary>
    public enum PlayerTurnEnum
    {
        /// <summary>
        /// No any actions
        /// </summary>
        None,
        /// <summary>
        /// Player should move to the left
        /// </summary>
        MoveLeft,
        /// <summary>
        /// Player should move to the right
        /// </summary>
        MoveRight,
        /// <summary>
        /// Player should move up
        /// </summary>
        MoveUp,
        /// <summary>
        /// Player should move down
        /// </summary>
        MoveDown,
        /// <summary>
        /// Player should put the bomb
        /// </summary>
        PutBomb
    }
}