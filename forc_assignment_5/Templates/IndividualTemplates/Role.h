#ifndef FORC_PA_5_ROLE_H
#define FORC_PA_5_ROLE_H

#include "string"
#include "iostream"
#include "IndividualBaseTemplate.h"


class Role: public IndividualBaseTemplate {

public:
    Role(baseIndividualTemplateStats* base_stats, vector<Offensive*>* offensive_actions, vector<Defensive*>* defensive_actions);
    Role(baseIndividualTemplateStats* base_stats);

    Range get_fear_range();
    Range get_terror_range();
    string get_raw_info();
    friend std::ostream& operator<< (std::ostream& out, Role* person);

private:
    int fear_min;
    int fear_max;
    int terror_min;
    int terror_max;
};


#endif //FORC_PA_5_ROLE_H
