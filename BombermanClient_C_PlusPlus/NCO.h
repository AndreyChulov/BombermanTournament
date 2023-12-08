//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_NCO_H
#define BOMBERMANCLIENT_C_PLUSPLUS_NCO_H

#include "./Core/Network.Shared/Interfaces/INetworkObject.h"
#include "./Core/Network.Shared/Contracts/Messages/BaseMessage.h"

using namespace Core_Network_Shared_Interfaces;
using namespace Core_Network_Shared_Contracts_Messages;

class NCO : public INetworkObject{
protected:
    template<typename T>
    void SendMessage(T* messageObject, BaseMessage* baseMessage) {};
};


#endif //BOMBERMANCLIENT_C_PLUSPLUS_NCO_H
