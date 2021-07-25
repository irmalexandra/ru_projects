#include "Species.h"

Species::Species(speciesStats* species_stats):BaseTemplate(species_stats){
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
    out << (BaseTemplate*)(Species);

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
