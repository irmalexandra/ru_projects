//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_INDIVIDUALCREATOR_H
#define FORC_PA_5_INDIVIDUALCREATOR_H

#include <random>
#include <string>
#include <map>
#include "../Models/Investigator.h"
#include "../Models/EldritchHorror.h"
#include "../Models/Creature.h"
#include "../Models/Person.h"
#include "../Models/Being.h"
#include "../Helpers/HelperFunctions.h"
#include "../Templates/IndividualTemplates/Species.h"
#include "../Templates/IndividualTemplates/Role.h"
#include "../Helpers/Structs/Payload.h"
#include "../Helpers/IndexFinder.h"

using namespace std;

class IndividualCreator {
public:

    IndividualCreator();
    ~IndividualCreator();

    Person* createPerson(Role* selected_role, Payload* payload);
    Investigator* createInvestigator(Role* selected_role, Payload* payload);
    Creature* createCreature(Species* selected_species, Payload* payload);
    EldritchHorror* createEldritchHorror(Species* selected_species, Payload* payload);
    string* get_individual_name(string template_type, string template_name, Payload* payload);

private:
    const int base_stat_count = 3;
    int* base_stats;


};


#endif //FORC_PA_5_INDIVIDUALCREATOR_H
