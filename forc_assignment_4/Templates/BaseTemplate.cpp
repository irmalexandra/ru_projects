#include "BaseTemplate.h"
#include "iostream"

int LOWER_LIMIT = 0;
int UPPER_LIMIT = 10;

BaseTemplate::BaseTemplate(baseStats* base_stats){
    this->name = base_stats->name;
    this->type = base_stats->type;
    this->life_min = base_stats->life_min;
    this->life_max = base_stats->life_max;
    this->intelligence_min = base_stats->int_min;
    this->intelligence_max = base_stats->int_max;
    this->strength_min = base_stats->str_min;
    this->strength_max = base_stats->str_max;
}

std::string BaseTemplate::get_name() {
    return this->name;
}

Range BaseTemplate::get_life_range() {
    return Range(life_min, life_max);
}

Range BaseTemplate::get_intelligence_range() {
    return Range(intelligence_min, intelligence_max);
}

Range BaseTemplate::get_strength_range() {
    return Range(strength_min, strength_max);
}

void BaseTemplate::set_life_range(int* life_min, int* life_max) {
    this->life_min = *life_min;
    this->life_max = *life_max;
}

void BaseTemplate::set_intelligence_range(int* intelligence_min, int* intelligence_max) {
    this->intelligence_min = *intelligence_min;
    this->intelligence_max = *intelligence_max;
}

void BaseTemplate::set_strength_range(int* strength_min, int* strength_max) {
    this->strength_min = *strength_min;
    this->strength_max = *strength_max;
}

void BaseTemplate::set_name(std::string* name) {
    this->name = *name;
}



std::ostream& operator<< (std::ostream& out, BaseTemplate* BaseTemplate) {
    out << "Name: " << BaseTemplate->get_name() << std::endl;
    out << BaseTemplate->get_type() << std::endl;
    out << "Life: " << BaseTemplate->get_life_range() << std::endl;
    out << "Strength: " << BaseTemplate->get_strength_range() << std::endl;
    out << "Intelligence: " << BaseTemplate->get_intelligence_range() << std::endl;
    return out;
}

void BaseTemplate::set_type(std::string* type) {
    this->type = *type;
}

std::string BaseTemplate::get_type() {
    return this->type;
}


