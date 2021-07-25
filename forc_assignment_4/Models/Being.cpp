#include "Being.h"
#include "../Helpers/HelperFunctions.h"
#include "iostream"

Being::Being(BaseTemplate* base_template){
    this->life = get_random_integer(base_template->get_life_range());
    this->strength = get_random_integer(base_template->get_strength_range());
    this->intelligence = get_random_integer(base_template->get_intelligence_range());

    this->base_template = base_template;
}

Being::Being(baseIndividualStats *stats) {
    this->life = stats->life;
    this->strength = stats->strength;
    this->intelligence = stats->intelligence;
    this->is_investigator = false;

}

Being::Being(baseIndividualStats *stats, BaseTemplate* base_template) {
    this->life = stats->life;
    this->strength = stats->strength;
    this->intelligence = stats->intelligence;
    this->is_investigator = false;

    this->base_template = base_template;
}

void Being::set_name(std::string* name){
    this->name = *name;

    delete name;
    name = nullptr;
}

BaseTemplate* Being::get_template() {
    return this->base_template;
}

std::string Being::get_name() {
    return this->name;
}

int Being::get_life() {
    return this->life;
}

int Being::get_intelligence() {
    return this->intelligence;
}

int Being::get_strength() {
    return this->strength;
}

void Being::set_life(int* life) {
    this->life = *life;

    delete life;
    life = nullptr;
}

void Being::set_intelligence(int* intelligence) {
    this->intelligence = *intelligence;

    delete intelligence;
    intelligence = nullptr;
}

void Being::set_strength(int* strength) {
    this->strength = *strength;

    delete strength;
    strength = nullptr;
}

bool Being::get_is_investigator() {
    return this->is_investigator;
}

void Being::set_is_investigator(bool *is_investigator) {
    this->is_investigator = *is_investigator;

    delete is_investigator;
    is_investigator = nullptr;
}

std::ostream& operator<< (std::ostream& out, Being* being) {
    out << "Name: " << being->get_name() << std::endl;
    if (being->get_is_investigator()){
        out << "Investigator" << std::endl;
    }else{
        out << being->get_template()->get_type() << std::endl;
    }
    out << being->get_template()->get_name() << std::endl;
    out << "Life: " << being->get_life() << std::endl;
    out << "Strength: " << being->get_strength() << std::endl;
    out << "Intelligence: " << being->get_intelligence() << std::endl;

    return out;
}

