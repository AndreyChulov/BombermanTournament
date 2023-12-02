//
// Created by Andrey on 02.12.2023.
//

#include "IPositionItem.h"

namespace Games_BombermanGame_Shared_Interfaces {
    int IPositionItem::GetX() const {
        return _x;
    }

    int IPositionItem::GetY() const {
        return _y;
    }

    IPositionItem::IPositionItem(int x, int y) : _x(x), _y(y) {}
} // Games_BombermanGame_Shared_Interfaces