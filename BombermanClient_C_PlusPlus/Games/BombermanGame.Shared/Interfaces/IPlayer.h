//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_IPLAYER_H
#define BOMBERMANCLIENT_C_PLUSPLUS_IPLAYER_H

#include <string>

#include "../Enums/FieldItemEnum.h"
#include "../Enums/PlayerTurnEnum.h"
#include "IGameInfo.h"
#include "IPlayerInfo.h"

namespace Games_BombermanGame_Shared_Interfaces {

    using namespace Games_BombermanGame_Shared_Enums;
    using namespace std;

    class IPlayer {
    private:
        string _nickname;
        string _strategyDescription;
        bool _isDebugMode;
        string _aiDevelopedForGame;
    public:
        explicit IPlayer(string nickname,
                         string strategyDescription,
                         bool isDebugMode,
                         string aiDevelopedForGame);

        /// <summary>
        /// Short name for AI player
        /// </summary>
        const string GetNickname() const;

        /// <summary>
        /// AI strategy description for tournament information
        /// </summary>
        const string GetStrategyDescription() const;

        /// <summary>
        /// Is bot in debug mode (disabled timeout for turn)
        /// </summary>
        bool GetIsDebugMode() const;

        /// <summary>
        /// The game name for which Ai/bot was developed
        /// </summary>
        const string GetAiDevelopedForGame() const;

    public:
        /// <summary>
        /// Method which is used for turn calculation
        /// </summary>
        /// <param name="gameInfo">Whole game information that can be required</param>
        /// <param name="currentPlayerInfo">Info about current player</param>
        /// <param name="parallelLoopState"></param>
        /// <returns>Enum that describe current player turn request</returns>
        virtual PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo) = 0;
        virtual void OnTurnTimeExceeded() = 0;
    };

} // Games_BombermanGame_Shared_Interfaces

#endif //BOMBERMANCLIENT_C_PLUSPLUS_IPLAYER_H
