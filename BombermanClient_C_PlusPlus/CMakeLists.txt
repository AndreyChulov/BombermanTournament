cmake_minimum_required(VERSION 3.26)

project(BombermanClient_C_PlusPlus)

set(CMAKE_CXX_STANDARD 17)


add_executable(BombermanClient_C_PlusPlus main.cpp
        Games/BombermanGame.Shared/Interfaces/IPlayer.cpp
        Games/BombermanGame.Shared/Interfaces/IPlayer.h
        Games/BombermanGame.Shared/Enums/FieldItemEnum.h
        Games/BombermanGame.Shared/Enums/PlayerTurnEnum.h
        Games/BombermanGame.Shared/Interfaces/IGameInfo.cpp
        Games/BombermanGame.Shared/Interfaces/IGameInfo.h
        Games/BombermanGame.Shared/Interfaces/IPlayerInfo.cpp
        Games/BombermanGame.Shared/Interfaces/IPlayerInfo.h
        Games/BombermanGame.Shared/Interfaces/IPositionItem.cpp
        Games/BombermanGame.Shared/Interfaces/IPositionItem.h
        Bot.cpp
        Bot.h
        Client.cpp
        Client.h
        Core/Network.Shared/Interfaces/INetworkClientObject.cpp
        Core/Network.Shared/Interfaces/INetworkClientObject.h
        Core/Network.Shared/Interfaces/INetworkObject.cpp
        Core/Network.Shared/Interfaces/INetworkObject.h
        Core/Network.Shared/Contracts/Messages/BaseMessage.cpp
        Core/Network.Shared/Contracts/Messages/BaseMessage.h
        NCO.cpp
        NCO.h)

