//
// Created by emmik on 22/03/2021.
//

#include "IndividualCreator.h"

IndividualCreator::IndividualCreator(){
    this->base_stats = new int[this->base_stat_count];
}

IndividualCreator::~IndividualCreator() {
    delete this->base_stats;
    this->base_stats = nullptr;
}

Person *IndividualCreator::createPerson(Role* selected_role, Payload* payload) {
    return new Person(
            this->get_individual_name(selected_role->get_type(), selected_role->get_name(), payload)
            , gender_picker() , selected_role);
}

Investigator *IndividualCreator::createInvestigator(Role* selected_role, Payload* payload) {
    return new Investigator(
            this->get_individual_name(selected_role->get_type(), selected_role->get_name(), payload)
            , gender_picker(), selected_role);
}

Creature *IndividualCreator::createCreature(Species* selected_species, Payload* payload) {
    return new Creature(
            this->get_individual_name(selected_species->get_type(), selected_species->get_name(), payload),
            selected_species);
}

EldritchHorror *IndividualCreator::createEldritchHorror(Species* selected_species, Payload* payload) {
    return new EldritchHorror(
            this->get_individual_name(selected_species->get_type(), selected_species->get_name(), payload),
            selected_species);
}

string* IndividualCreator::get_individual_name(string template_type, string template_name, Payload* payload){
    auto new_name = new string(template_name);
    if (template_type == "Person" || template_type == "Investigator"){
        cout << "Enter the name of the character you are creating." << endl;
        cin >> *new_name;
        while(get_index(payload->DHPersons->get_data(), *new_name) != -1 || get_index(payload->DHInvestigators->get_data(), *new_name) != -1 ||
                get_index(payload->DHSpecies->get_data(), *new_name) != -1 || get_index(payload->DHEldritchHorrors->get_data(), *new_name) != -1){
            cout << "Name already taken, choose a different name." << endl;
            cin >> *new_name;
        }
    }
    else{
        *new_name = template_name;
    }

    return new_name;
}






