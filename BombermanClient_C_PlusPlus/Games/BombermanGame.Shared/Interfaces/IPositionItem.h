//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_IPOSITIONITEM_H
#define BOMBERMANCLIENT_C_PLUSPLUS_IPOSITIONITEM_H

namespace Games_BombermanGame_Shared_Interfaces {

    class IPositionItem {
    private:
        int _x;
        int _y;

    public:
        int GetX() const;
        int GetY() const;

    public:
        explicit IPositionItem(int x, int y);
    };

} // Games_BombermanGame_Shared_Interfaces

#endif //BOMBERMANCLIENT_C_PLUSPLUS_IPOSITIONITEM_H
