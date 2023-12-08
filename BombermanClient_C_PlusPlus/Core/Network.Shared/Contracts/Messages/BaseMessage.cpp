//
// Created by Andrey on 02.12.2023.
//

#include "BaseMessage.h"

namespace Core_Network_Shared_Contracts_Messages {
    const string BaseMessage::GetMessage() const {
        return _message;
    }

    void BaseMessage::SetMessage(const string &message) {
        _message = message;
    }
} // Core_Network_Shared_Contracts_Messages