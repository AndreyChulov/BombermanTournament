//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_IGAMEINFO_H
#define BOMBERMANCLIENT_C_PLUSPLUS_IGAMEINFO_H

#include <string>

#include "../Enums/FieldItemEnum.h"
#include "../Enums/PlayerTurnEnum.h"
#include "IPlayerInfo.h"

namespace Games_BombermanGame_Shared_Interfaces {

    using namespace Games_BombermanGame_Shared_Enums;
    using namespace std;

    class IGameInfo {
    private:
        FieldItemEnum** _field;
        int _fieldHeight;
        int _fieldWidth;
        IPlayerInfo* _playersInfos;

    public:
        FieldItemEnum **getField() const;
        int getFieldHeight() const;
        int getFieldWidth() const;
        IPlayerInfo *getPlayersInfos() const;
    };

} // Games_BombermanGame_Shared_Interfaces

#endif //BOMBERMANCLIENT_C_PLUSPLUS_IGAMEINFO_H
