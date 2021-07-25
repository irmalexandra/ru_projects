
#ifndef FORC_PA_4_ROLE_H
#define FORC_PA_4_ROLE_H


#include "BaseTemplate.h"
#include "string"
#include "iostream"

class Role: public BaseTemplate {
public:
    Role(baseStats* base_stats);

    Range get_fear_range();
    Range get_terror_range();

    friend std::ostream& operator<< (std::ostream& out, Role* person);

private:
    int fear_min;
    int fear_max;
    int terror_min;
    int terror_max;
};


#endif //FORC_PA_4_ROLE_H
