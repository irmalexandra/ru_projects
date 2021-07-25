#ifndef FORC_PA_4_SPECIES_H
#define FORC_PA_4_SPECIES_H

#include "BaseTemplate.h"
#include "iostream"


struct speciesStats : public baseStats {
    bool is_eldritch = false;

    bool unnatural = 0;
    int dis_min = 0;
    int dis_max = 0;
    int trauma_min = 0 ;
    int trauma_max = 0;
};

class Species: public BaseTemplate {
public:
    explicit Species() = default;
    explicit Species(speciesStats* species_stats);


    bool get_unnatural();
    Range get_disquiet_range();
    Range get_traumatism_range();
    bool get_is_eldritch();

    void set_unnatural(bool* unnatural);
    void set_disquiet_range(int* disquiet_min, int* disquiet_max);
    void set_traumatism_range(int* traumatism_min, int* traumatism_max);

    friend std::ostream& operator<< (std::ostream& out, Species* creature);

private:
    bool unnatural;
    int disquiet_min;
    int disquiet_max;
    bool is_eldritch_horror;
    int traumatism_min;
    int traumatism_max;
};

#endif //FORC_PA_4_SPECIES_H
