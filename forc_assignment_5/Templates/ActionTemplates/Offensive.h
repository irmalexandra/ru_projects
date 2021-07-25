

#ifndef FORC_PA_5_OFFENSIVE_H
#define FORC_PA_5_OFFENSIVE_H

#include "ActionBaseTemplate.h"

using namespace std;

class Offensive: public ActionBaseTemplate {
public:
    Offensive(baseActionTemplateStats* stats, int* hit_modifier, int* damage);
    int get_hit_modifier();
    int get_damage();

    friend std::ostream& operator<< (std::ostream& out, Offensive* offensive);


private:
    int hit_modifier;
    int damage;
};


#endif //FORC_PA_5_OFFENSIVE_H
