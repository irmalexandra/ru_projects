#ifndef FORC_PA_5_DEFENSIVE_H
#define FORC_PA_5_DEFENSIVE_H

#include "ActionBaseTemplate.h"


using namespace std;

class Defensive : public ActionBaseTemplate {
public:
    explicit Defensive(baseActionTemplateStats* stats, int* def_modifier = nullptr, int* atk_modifier = nullptr, int* hp_recovery = nullptr, int* duration = nullptr);
    int get_def_modifier();
    int get_atk_modifier();
    int get_recovery();
    int get_duration();

    friend std::ostream& operator<< (std::ostream& out, Defensive* defensive);
private:
    int defense_modifier;
    int attack_modifier;
    int health_recovery;
    int duration; // Number of turns

};


#endif //FORC_PA_5_DEFENSIVE_H
