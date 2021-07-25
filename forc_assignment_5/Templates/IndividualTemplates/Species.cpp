#include "Species.h"


Species::Species(speciesStats* species_stats, vector<Offensive*>* offensive_actions, vector<Defensive*>* defensive_actions):
IndividualBaseTemplate(species_stats, offensive_actions, defensive_actions){

    this->unnatural = species_stats->unnatural;
    this->disquiet_min = species_stats->dis_min;
    this->disquiet_max = species_stats->dis_max;
    this->is_eldritch_horror = species_stats->is_eldritch;
    this->traumatism_min = species_stats->trauma_min;
    this->traumatism_max = species_stats->trauma_max;
}

Species::Species(speciesStats *species_stats): IndividualBaseTemplate(species_stats) {
    this->unnatural = species_stats->unnatural;
    this->disquiet_min = species_stats->dis_min;
    this->disquiet_max = species_stats->dis_max;
    this->is_eldritch_horror = species_stats->is_eldritch;
    this->traumatism_min = species_stats->trauma_min;
    this->traumatism_max = species_stats->trauma_max;
}

bool Species::get_unnatural() {
    return this->unnatural;
}

Range Species::get_disquiet_range() {
    return Range(this->disquiet_min, this->disquiet_max);
}

Range Species::get_traumatism_range(){
    return Range(this->traumatism_min, this->traumatism_max);
}

bool Species::get_is_eldritch() {
    return this->is_eldritch_horror;
}

void Species::set_unnatural(bool* unnatural) {
    this->unnatural = *unnatural;
}

void Species::set_disquiet_range(int* disquiet_min, int* disquiet_max) {
    this->disquiet_min = *disquiet_min;
    this->disquiet_max = *disquiet_max;

}

void Species::set_traumatism_range(int* traumatism_min, int* traumatism_max){
    this->traumatism_min = *traumatism_min;
    this->traumatism_max = *traumatism_max;
}

std::ostream& operator<< (std::ostream& out, Species* Species){

    out << (IndividualBaseTemplate*)(Species);


    if(Species->get_unnatural()){
        out << "Unnatural" << std::endl;
    }else{
        out << "Natural" << std::endl;
    }


    if (Species->is_eldritch_horror){
        out << "Disquiet: 10-10" << std::endl;
        out << "Traumatism: " << Species->get_traumatism_range() << std::endl;
    }
    else {
        out << "Disquiet: " << Species->get_disquiet_range() << std::endl;
    }

    return out;
};


string Species::get_raw_info() {
    string return_string;
    return_string += "Name: " + get_name() +  '\n';
    return_string += get_type() +  '\n';
    return_string += "Life: " + to_string(get_life_range().min) + "-" + to_string(get_life_range().max) +  '\n';
    return_string +=  "Strength: " + to_string(get_strength_range().min) + "-" + to_string(get_strength_range().max) +  '\n';
    return_string +=  "Intelligence: " + to_string(get_intelligence_range().min) + "-" + to_string(get_intelligence_range().max) +  '\n';
    return_string +=  "Offensive actions count: " + to_string(offensive_actions->size()) +  '\n';
    for (int i = 0; i < offensive_actions->size(); i++){
        return_string +=  offensive_actions->at(i)->get_name() +  '\n';
    }
    return_string +=  "Defensive actions count: " + to_string(defensive_actions->size()) +  '\n';
    for (int i = 0; i < defensive_actions->size(); i++){
        return_string +=  defensive_actions->at(i)->get_name() +  '\n';
    }
    if(this->get_unnatural()){
        return_string += "Unnatural\n";
    }else{
        return_string += "Natural\n";
    }
    if (this->is_eldritch_horror){
        return_string += "Disquiet: 10-10\n";
        return_string += "Traumatism: " + to_string(this->get_traumatism_range().min) + "-" + to_string(this->get_traumatism_range().max) + "\n\n";
    }
    else {
        return_string += "Disquiet: " + to_string(this->get_disquiet_range().min) + "-" + to_string(this->get_disquiet_range().max) + "\n\n";
    }
    return return_string;
}


