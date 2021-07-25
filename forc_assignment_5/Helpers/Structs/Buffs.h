#ifndef FORC_PA_5_BUFFS_H
#define FORC_PA_5_BUFFS_H

#include "iostream"
#include "vector"
#include "../../Templates/ActionTemplates/Defensive.h"

using namespace std;


struct Buff {
    explicit Buff(Defensive* action){
        this->action = action;
        this->duration_remaining = this->action->get_duration();
    }
public:
    Defensive* action;
    int duration_remaining;
};
#endif //FORC_PA_5_BUFFS_H
