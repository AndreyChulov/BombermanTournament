//
// Created by Andrey on 02.12.2023.
//

#include "Bot.h"

Bot::Bot() : IPlayer(
        NICKNAME,
        STRATEGY_DESCRIPTION,
        IS_DEBUG_MODE,
        AI_DEVELOPED_FOR_GAME) {}

PlayerTurnEnum Bot::Turn(IGameInfo gameInfo, IPlayerInfo currentPlayerInfo) {
    return MoveRight;
}

void Bot::OnTurnTimeExceeded() {

}
