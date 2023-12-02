//
// Created by Andrey on 02.12.2023.
//

#include "IPlayer.h"

namespace Games_BombermanGame_Shared_Interfaces {
    const string IPlayer::GetNickname() const {
        return this->_nickname;
    }

    const string IPlayer::GetStrategyDescription() const {
        return _strategyDescription;
    }

    bool IPlayer::GetIsDebugMode() const {
        return _isDebugMode;
    }

    const string IPlayer::GetAiDevelopedForGame() const {
        return _aiDevelopedForGame;
    }

    IPlayer::IPlayer(string nickname,
                     string strategyDescription,
                     bool isDebugMode,
                     string aiDevelopedForGame) :
            _nickname(nickname),
            _strategyDescription(strategyDescription),
            _isDebugMode(isDebugMode),
            _aiDevelopedForGame(aiDevelopedForGame) {}
} // Games_BombermanGame_Shared_Interfaces