#ifndef FORC_PA_5_PAYLOAD_H
#define FORC_PA_5_PAYLOAD_H

#include <map>
//
#include "../../Models/Investigator.h"
#include "../../Models/Person.h"
#include "../../Models/Creature.h"
#include "../../Models/EldritchHorror.h"
#include "../../Templates/IndividualTemplates/Species.h"
#include "../../Templates/IndividualTemplates/Role.h"
#include "../../Templates/ActionTemplates/Offensive.h"
#include "../../Templates/ActionTemplates/Defensive.h"
#include "../DataHandler.h"

struct Payload {
    Payload(
            DataHandler<Investigator>* dh_investigators,
            DataHandler<Person>* dh_persons,
            DataHandler<Creature>* dh_creatures,
            DataHandler<EldritchHorror>* dh_eldritch_horrors,
            DataHandler<Species>* dh_species,
            DataHandler<Role>* dh_roles,
            DataHandler<Offensive>* dh_offensive,
            DataHandler<Defensive>* dh_defensive,
            map<string, int>* species_map);

    // ROSTER
    DataHandler<Investigator>* DHInvestigators;
    DataHandler<Person>* DHPersons;
    DataHandler<Creature>* DHCreatures;
    DataHandler<EldritchHorror>* DHEldritchHorrors;


    DataHandler<Offensive>* DHOffensives;
    DataHandler<Defensive>* DHDefensives;


    // Templates
    DataHandler<Species>* DHSpecies;
    DataHandler<Role>* DHRoles;
//    DataHandler<Action>* DHActions;
    // Species Map
    std::map<string, int>* species_map;
};


#endif //FORC_PA_5_PAYLOAD_H
