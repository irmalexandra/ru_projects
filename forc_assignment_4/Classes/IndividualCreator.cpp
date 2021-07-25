#include "IndividualCreator.h"

IndividualCreator::IndividualCreator(){
    this->base_stats = new int[this->base_stat_count];
}

IndividualCreator::~IndividualCreator() {
    delete this->base_stats;
    this->base_stats = nullptr;
}

Person *IndividualCreator::createPerson(Role* selected_role) {
    return new Person(
            this->get_individual_name(selected_role->get_type(), selected_role->get_name())
            , gender_picker()
            , selected_role);
}

Investigator *IndividualCreator::createInvestigator(Role* selected_role) {
    return new Investigator(
            this->get_individual_name(selected_role->get_type(), selected_role->get_name())
            , gender_picker()
            , selected_role);
}

Creature *IndividualCreator::createCreature(Species* selected_species) {
    return new Creature(
            this->get_individual_name(selected_species->get_type(), selected_species->get_name()),
            selected_species);
}

EldritchHorror *IndividualCreator::createEldritchHorror(Species* selected_species) {
    return new EldritchHorror(
            this->get_individual_name(selected_species->get_type(), selected_species->get_name()),
            selected_species);
}

string* IndividualCreator::get_individual_name(string template_type, string template_name){
    auto new_name = new string(template_name);
    if (template_type == "Person" || template_type == "Investigator"){
        cout << "Enter the name of the character you are creating." << endl;
        cin >> *new_name;
    }
    else{
       *new_name = template_name;
    }

    return new_name;
}






