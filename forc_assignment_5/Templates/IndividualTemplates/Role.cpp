#include "Role.h"


Role::Role(baseIndividualTemplateStats* base_stats, vector<Offensive*>* offensive_actions, vector<Defensive*>* defensive_actions):
IndividualBaseTemplate(base_stats, offensive_actions, defensive_actions){

    this->fear_min = 0;
    this->fear_max = 10;
    this->terror_min = 0;
    this->terror_max = 3;
}

Role::Role(baseIndividualTemplateStats *base_stats): IndividualBaseTemplate(base_stats) {
    this->fear_min = 0;
    this->fear_max = 10;
    this->terror_min = 0;
    this->terror_max = 3;
}

std::ostream& operator<< (std::ostream& out, Role* Role) {
    out << (IndividualBaseTemplate*)(Role);
    return out;
}

Range Role::get_fear_range() {
    return {this->fear_min, this->fear_max};
}

Range Role::get_terror_range() {
    return {this->terror_min, this->terror_max};
}



string Role::get_raw_info() {
    string return_string;
    return_string += "Name: " + get_name() +  '\n';
    return_string += get_type() +  '\n';
    return_string += "Life: " + to_string(get_life_range().min) + "-" + to_string(get_life_range().max) +  '\n';
    return_string +=  "Strength: " + to_string(get_strength_range().min) + "-" + to_string(get_strength_range().max) +  '\n';
    return_string +=  "Intelligence: " + to_string(get_intelligence_range().min) + "-" + to_string(get_intelligence_range().max) +  '\n';
    return_string +=  "Offensive actions count: " + to_string(offensive_actions->size()) +  '\n';
    for (int i = 0; i < offensive_actions->size()-1; i++){
        return_string +=  offensive_actions->at(i)->get_name() +  '\n';
    }
    return_string +=  offensive_actions->back()->get_name() +  '\n';
    return_string +=  "Defensive actions count: " + to_string(defensive_actions->size()) +  '\n';
    for (int i = 0; i < defensive_actions->size()-1; i++){
        return_string +=  defensive_actions->at(i)->get_name() +  '\n';
    }
    return_string +=  defensive_actions->back()->get_name() +  "\n\n";

    return return_string;
}



