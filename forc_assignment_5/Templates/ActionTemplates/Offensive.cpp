
#include "Offensive.h"

Offensive::Offensive(baseActionTemplateStats* stats, int* hit_modifier, int* damage): ActionBaseTemplate(stats) {
    this->damage = *damage;
    this->hit_modifier = *hit_modifier;


}

int Offensive::get_damage() {
    return this->damage;
}

int Offensive::get_hit_modifier() {
    return this->hit_modifier;
}

std::ostream& operator<< (std::ostream& out, Offensive* offensive) {
    out << (ActionBaseTemplate*)(offensive);
    if (offensive->physical){
        out << "Physical" << endl;
    }
    else{
        out << "Mental" << endl;
    }
    out << "Hit Modifier: " << offensive->get_hit_modifier() << endl;
    out << "Damage: " << offensive->get_damage() << endl;

    return out;
}



