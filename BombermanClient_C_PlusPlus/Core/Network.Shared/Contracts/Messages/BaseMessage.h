//
// Created by Andrey on 02.12.2023.
//

#ifndef BOMBERMANCLIENT_C_PLUSPLUS_BASEMESSAGE_H
#define BOMBERMANCLIENT_C_PLUSPLUS_BASEMESSAGE_H

#include <string>

namespace Core_Network_Shared_Contracts_Messages {

    using namespace std;

    class BaseMessage {
    private:
        string _message;
    public:
        const string GetMessage() const;
        void SetMessage(const string &message);
    };

} // Core_Network_Shared_Contracts_Messages

#endif //BOMBERMANCLIENT_C_PLUSPLUS_BASEMESSAGE_H
