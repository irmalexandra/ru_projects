#include "ActionBaseTemplate.h"

ActionBaseTemplate::ActionBaseTemplate(baseActionTemplateStats *stats) {
    this->action_name = stats->name;
    this->physical = stats->physical;
    this->type = stats->type;
    this->cooldown = stats->cooldown;
    this->cooldown_remaining = 0;
}

void ActionBaseTemplate::use_action() {
    if (cooldown_remaining < 1){
        this->cooldown_remaining = cooldown;
    }
}

int ActionBaseTemplate::get_cooldown(){
    return this->cooldown;
}
int ActionBaseTemplate::get_cooldown_remaining(){
    return this->cooldown_remaining;
}
bool ActionBaseTemplate::is_ready(){
    return this->cooldown_remaining < 1;
}
void ActionBaseTemplate::decrement_cooldown_remaining(){
    if (this->cooldown_remaining < 1){ return; }
    this->cooldown_remaining--;
}

std::string ActionBaseTemplate::get_name() {
    return this->action_name;
}

std::string ActionBaseTemplate::get_type() {
    return this->type;
}

std::ostream& operator<< (std::ostream& out, ActionBaseTemplate* actionBaseTemplate) {
    out << "Name: " << actionBaseTemplate->get_name() << std::endl;
    out << "Type: " << actionBaseTemplate->get_type() << std::endl;
    out << "Cooldown: " << actionBaseTemplate->get_cooldown() << std::endl;
    return out;
}

bool ActionBaseTemplate::is_physical() {
    return this->physical;
}


