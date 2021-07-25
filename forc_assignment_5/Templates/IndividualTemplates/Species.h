#ifndef FORC_PA_5_SPECIES_H
#define FORC_PA_5_SPECIES_H

#include "IndividualBaseTemplate.h"
#include "iostream"

struct speciesStats : public baseIndividualTemplateStats {
    bool is_eldritch = false;

    bool unnatural = 0;
    int dis_min = 0;
    int dis_max = 0;
    int trauma_min = 0 ;
    int trauma_max = 0;
};


class Species: public IndividualBaseTemplate {

public:
    explicit Species(speciesStats* species_stats, vector<Offensive*>* offensive_actions, vector<Defensive*>* defensive_actions);
    explicit Species(speciesStats* species_stats);


    bool get_unnatural();
    Range get_disquiet_range();
    Range get_traumatism_range();
    bool get_is_eldritch();

    void set_unnatural(bool* unnatural);
    void set_disquiet_range(int* disquiet_min, int* disquiet_max);
    void set_traumatism_range(int* traumatism_min, int* traumatism_max);
    string get_raw_info();
    friend std::ostream& operator<< (std::ostream& out, Species* creature);

private:
    bool unnatural;
    int disquiet_min;
    int disquiet_max;
    bool is_eldritch_horror;
    int traumatism_min;
    int traumatism_max;
};


#endif //FORC_PA_5_SPECIES_H
