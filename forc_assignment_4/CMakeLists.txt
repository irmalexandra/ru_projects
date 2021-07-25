cmake_minimum_required(VERSION 3.17)
project(FORC_PA_4)

set(CMAKE_CXX_STANDARD 14)

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR})

add_executable(FORC_PA_4 main.cpp
        Models/Being.cpp
        Models/Being.h
        Models/Creature.cpp
        Models/Creature.h
        Models/Person.cpp
        Models/Person.h
        Models/Investigator.cpp
        Models/Investigator.h
        Models/EldritchHorror.cpp
        Models/EldritchHorror.h
        Classes/IndividualCreator.cpp
        Classes/IndividualCreator.h
        Helpers/InputHandler.cpp
        Helpers/InputHandler.h
        Helpers/FileHandler.cpp
        Helpers/FileHandler.h
        Helpers/HelperFunctions.cpp
        Helpers/HelperFunctions.h
        Helpers/DataHandler.h
        Classes/TemplateCreator.cpp
        Classes/TemplateCreator.h
        Templates/BaseTemplate.cpp
        Templates/BaseTemplate.h
        Templates/Species.cpp
        Templates/Species.h
        Templates/Role.cpp
        Templates/Role.h
        )