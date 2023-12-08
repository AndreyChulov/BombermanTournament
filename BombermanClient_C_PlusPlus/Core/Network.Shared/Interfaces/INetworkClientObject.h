//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_INETWORKCLIENTOBJECT_H
#define BOMBERMANCLIENT_C_PLUSPLUS_INETWORKCLIENTOBJECT_H

#include "INetworkObject.h"
#include "../Contracts/Messages/BaseMessage.h"

namespace Core_Network_Shared_Interfaces {

    using namespace Core_Network_Shared_Contracts_Messages;

    class INetworkClientObject : public INetworkObject{
    protected:
        template<typename T>
        void SendMessage(T* messageObject, BaseMessage* baseMessage) {};

    public:
        virtual void StartClient() = 0;
        virtual void StopClient() = 0;
        virtual void SetOnMessageReceivedAction(void (*onMessageReceived)(BaseMessage, string) ) = 0;

    public:
        template<typename T>
        void SendMessage(T* messageObject) {
            SendMessage(messageObject, (BaseMessage*) messageObject);
        };
    };

} // Core_Network_Shared_Interfaces

#endif //BOMBERMANCLIENT_C_PLUSPLUS_INETWORKCLIENTOBJECT_H
