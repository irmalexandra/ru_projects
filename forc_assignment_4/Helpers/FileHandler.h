#ifndef FORC_PA_4_FILEHANDLER_H
#define FORC_PA_4_FILEHANDLER_H
#include <random>
#include <vector>
#include <string>
#include <fstream>
#include <cstring>
#include <iostream>
#include <map>

#include "../Templates/BaseTemplate.h"
#include "../Templates/Role.h"
#include "../Templates/Species.h"
#include "../Helpers/HelperFunctions.h"
#include "DataHandler.h"


using namespace std;

struct Payload {
    Payload(
            DataHandler<Investigator>* dh_investigators,
            DataHandler<Person>* dh_persons,
            DataHandler<Creature>* dh_creatures,
            DataHandler<EldritchHorror>* dh_eldritch_horrors,
            DataHandler<Species>* dh_species,
            DataHandler<Role>* dh_roles,
            map<string, int>* species_map)

            {
        this->DHInvestigators = dh_investigators;
        this->DHPersons = dh_persons;
        this->DHCreatures = dh_creatures;
        this->DHEldritch_Horrors = dh_eldritch_horrors;
        this->DHSpecies = dh_species;
        this->DHRoles = dh_roles;
        this->species_map = species_map;

    }

    // ROSTER
    DataHandler<Investigator>* DHInvestigators;
    DataHandler<Person>* DHPersons;
    DataHandler<Creature>* DHCreatures;
    DataHandler<EldritchHorror>* DHEldritch_Horrors;

    // Templates
    DataHandler<Species>* DHSpecies;
    DataHandler<Role>* DHRoles;

    // Species Map
    map<string, int>* species_map;
};

class FileHandler {
public:
    void load_templates(Payload* payload);
    void save_templates(Payload* payload);
    void load_roster(Payload* payload, string* roster_name);
    void save_roster(Payload* payload, string* roster_name);

};



#endif //FORC_PA_4_FILEHANDLER_H
