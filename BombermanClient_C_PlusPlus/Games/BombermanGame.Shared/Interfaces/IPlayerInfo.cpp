//
// Created by Andrey on 02.12.2023.
//

#include "IPlayerInfo.h"

namespace Games_BombermanGame_Shared_Interfaces {
    int IPlayerInfo::GetScore() const {
        return _score;
    }

    const string IPlayerInfo::GetNickname() const {
        return _nickname;
    }

    void IPlayerInfo::SetOnScoreUpdated(void (*onScoreUpdated)()) {
        _onScoreUpdated = onScoreUpdated;
    }

    IPlayerInfo::IPlayerInfo(int x, int y, int score, string nickname) :
            IPositionItem(x, y),
            _score(score),
            _nickname(nickname) {}
} // Games_BombermanGame_Shared_Interfaces