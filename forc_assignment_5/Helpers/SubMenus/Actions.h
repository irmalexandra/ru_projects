//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_ACTIONS_H
#define FORC_PA_5_ACTIONS_H

#include "../../Classes/ActionCreator.h"
#include "iostream"
#include "../Structs/Payload.h"
#include "../FileHandler.h"

using namespace std;


void action_menu(FileHandler* file_handler, ActionCreator* action_creator, Payload* payload);
void create_action(FileHandler* file_handler, ActionCreator* action_creator, Payload* payload);
void view_actions(Payload* payload);
void delete_action(FileHandler* file_handler, Payload* payload);



#endif //FORC_PA_5_ACTIONS_H
