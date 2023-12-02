//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_BOT_H
#define BOMBERMANCLIENT_C_PLUSPLUS_BOT_H

#include "./Games/BombermanGame.Shared/Interfaces/IPlayer.h"
#include "./Games/BombermanGame.Shared/Interfaces/IGameInfo.h"
#include "./Games/BombermanGame.Shared/Interfaces/IPlayerInfo.h"

using namespace Games_BombermanGame_Shared_Interfaces;

class Bot : public IPlayer{
private:
    const string NICKNAME = "Player 1";
    const string STRATEGY_DESCRIPTION = "No strategy";
    const bool IS_DEBUG_MODE = true;
    const string AI_DEVELOPED_FOR_GAME = "Bomberman";

public:
    Bot();
    PlayerTurnEnum Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo) override;
    void OnTurnTimeExceeded() override;
};


#endif //BOMBERMANCLIENT_C_PLUSPLUS_BOT_H
