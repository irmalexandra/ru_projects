#ifndef FORC_PA_5_BATTLEHANDLER_H
#define FORC_PA_5_BATTLEHANDLER_H

#include <iostream>
#include <algorithm>
#include <vector>
#include "../Helpers/Structs/Payload.h"
#include "../Models/Being.h"
#include "../Helpers/IndexFinder.h"
#include "../Helpers/HelperFunctions.h"

using namespace std;

class BattleHandler {
public:
    explicit BattleHandler(Payload* payload);
    void decrement_cooldowns();
    void set_turn_order();
    void execute_ai_turn();
    void execute_offensive_action(Being* participant, string target_name, string action_name);
    void execute_defensive_action(Being* participant, string action_name);
    void start();
    void set_status();
    string find_target();
    string find_action();

private:
    int turn_tracker = 0;
    int turn_size;
    int round_tracker = 0;
    int monster_team_count = 0;
    int investigator_team_count = 0;
    vector<Being*>* participant_list;
    Payload* payload;
};

#endif //FORC_PA_5_BATTLEHANDLER_H
