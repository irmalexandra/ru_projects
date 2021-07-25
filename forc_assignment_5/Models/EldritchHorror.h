//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_ELDRITCHHORROR_H
#define FORC_PA_5_ELDRITCHHORROR_H


#include "Creature.h"
#include "iostream"
#include "../Templates/IndividualTemplates/Species.h"
#include "../Helpers/IndexFinder.h"

class EldritchHorror: public Creature{

public:
    explicit EldritchHorror(std::string* name, Species* species);
    EldritchHorror(baseIndividualStats* stats, Species* species);

    int get_traumatism();
    void edit(vector<EldritchHorror*>* existing_horrors);
    void set_traumatism(int* traumatism);

    friend std::ostream& operator<< (std::ostream& out, EldritchHorror* eldritchHorror);
    void reset();

private:
    int traumatism;
};


#endif //FORC_PA_5_ELDRITCHHORROR_H
