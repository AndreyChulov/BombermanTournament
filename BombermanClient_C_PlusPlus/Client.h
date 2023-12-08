//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_CLIENT_H
#define BOMBERMANCLIENT_C_PLUSPLUS_CLIENT_H

#include "Bot.h"

class Client {
private:
    Bot* _bot;
    INetworkClientObject* _networkClient;
public:
    explicit Client(Bot *bot);
public:


};


#endif //BOMBERMANCLIENT_C_PLUSPLUS_CLIENT_H
