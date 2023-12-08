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
#define NICKNAME "Player 1"
#define STRATEGY_DESCRIPTION "No strategy"
#define IS_DEBUG_MODE true
#define AI_DEVELOPED_FOR_GAME "Bomberman"

public:
    Bot();
    PlayerTurnEnum Turn(IGameInfo* gameInfo, IPlayerInfo* currentPlayerInfo) override;
    void OnTurnTimeExceeded() override;
};


#endif //BOMBERMANCLIENT_C_PLUSPLUS_BOT_H
