#include "Role.h"

Role::Role(baseStats* base_stats):BaseTemplate(base_stats){
    this->fear_min = 0;
    this->fear_max = 10;
    this->terror_min = 0;
    this->terror_max = 3;
}

std::ostream& operator<< (std::ostream& out, Role* Role) {
    out << (BaseTemplate*)(Role);
    return out;
}

Range Role::get_fear_range() {
    return {this->fear_min, this->fear_max};
}

Range Role::get_terror_range() {
    return {this->terror_min, this->terror_max};
}

