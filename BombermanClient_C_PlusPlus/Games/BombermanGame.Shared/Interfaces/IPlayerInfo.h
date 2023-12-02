//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_IPLAYERINFO_H
#define BOMBERMANCLIENT_C_PLUSPLUS_IPLAYERINFO_H

#include <string>

#include "IPositionItem.h"

namespace Games_BombermanGame_Shared_Interfaces {

    using namespace std;

    class IPlayerInfo : public IPositionItem {
    private:
        int _score;
        string _nickname;
        void(*_onScoreUpdated)();

    public:
        int GetScore() const;
        const string GetNickname() const;
        void SetOnScoreUpdated(void (*onScoreUpdated)());

    public:
        IPlayerInfo(int x, int y, int score, string nickname);
        virtual void BlowUpPlayer() = 0;
        virtual void PlayerBlowUpDestroyableCell() = 0;
    };

} // Games_BombermanGame_Shared_Interfaces

#endif //BOMBERMANCLIENT_C_PLUSPLUS_IPLAYERINFO_H
