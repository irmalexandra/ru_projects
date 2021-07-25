#ifndef FORC_PA_4_INDIVIDUALCREATOR_H
#define FORC_PA_4_INDIVIDUALCREATOR_H

#include <random>
#include <string>
#include <map>
#include "../Models/Investigator.h"
#include "../Models/EldritchHorror.h"
#include "../Models/Creature.h"
#include "../Models/Person.h"
#include "../Models/Being.h"
#include "../Helpers/HelperFunctions.h"
#include "../Templates/Species.h"
#include "../Templates/Role.h"

using namespace std;

class IndividualCreator {
public:

    IndividualCreator();
    ~IndividualCreator();

    Person* createPerson(Role* selected_role);
    Investigator* createInvestigator(Role* selected_role);
    Creature* createCreature(Species* selected_species);
    EldritchHorror* createEldritchHorror(Species* selected_species);
    string* get_individual_name(string template_type, string template_name);

private:
    const int base_stat_count = 3;
    int* base_stats;


};

#endif //FORC_PA_4_INDIVIDUALCREATOR_H