//
// Created by emmik on 23/03/2021.
//

#ifndef FORC_PA_5_ACTIONBASETEMPLATE_H
#define FORC_PA_5_ACTIONBASETEMPLATE_H

#include "string"
#include "../../Helpers/Structs/Stats.h"


class ActionBaseTemplate {

public:
    explicit ActionBaseTemplate(baseActionTemplateStats* stats);

    void use_action();
    int get_cooldown();
    int get_cooldown_remaining();
    bool is_ready();
    void decrement_cooldown_remaining();

    friend std::ostream& operator<< (std::ostream& out, ActionBaseTemplate* actionBaseTemplate);
    std::string get_name();
    std::string get_type();

    bool is_physical();




protected:
    std::string type;
    bool physical;
    int cooldown;
    int cooldown_remaining;

private:
    std::string action_name;
};


#endif //FORC_PA_5_ACTIONBASETEMPLATE_H
