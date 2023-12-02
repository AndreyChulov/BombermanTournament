//
// Created by Andrey on 02.12.2023.
//

#include "IGameInfo.h"

namespace Games_BombermanGame_Shared_Interfaces {
    FieldItemEnum **IGameInfo::getField() const {
        return _field;
    }

    int IGameInfo::getFieldHeight() const {
        return _fieldHeight;
    }

    int IGameInfo::getFieldWidth() const {
        return _fieldWidth;
    }

    IPlayerInfo *IGameInfo::getPlayersInfos() const {
        return _playersInfos;
    }
} // Games_BombermanGame_Shared_Interfaces