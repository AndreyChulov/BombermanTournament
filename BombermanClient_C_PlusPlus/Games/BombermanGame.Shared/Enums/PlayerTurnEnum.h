//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_PLAYERTURNENUM_H
#define BOMBERMANCLIENT_C_PLUSPLUS_PLAYERTURNENUM_H

namespace Games_BombermanGame_Shared_Enums {

    /// <summary>
    /// Enum that describe current player turn request
    /// </summary>
    enum PlayerTurnEnum
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

} // Games_BombermanGame_Shared_Enums

#endif //BOMBERMANCLIENT_C_PLUSPLUS_PLAYERTURNENUM_H
