#ifndef FORC_PA_5_INPUTHANDLER_H
#define FORC_PA_5_INPUTHANDLER_H


#include <vector>
#include <algorithm>
#include <iostream>
#include <fstream>
#include <map>

#include "../Models/Investigator.h"
#include "../Models/Person.h"
#include "../Models/Creature.h"
#include "../Models/EldritchHorror.h"
#include "../Classes/IndividualCreator.h"
#include "../Classes/TemplateCreator.h"
#include "../Classes/ActionCreator.h"
#include "DataHandler.h"

#include "../Templates/IndividualTemplates/Role.h"
#include "../Templates/IndividualTemplates/Species.h"
#include "../Templates/ActionTemplates/Offensive.h"
#include "../Templates/ActionTemplates/Defensive.h"


#include "Structs/Payload.h"
#include "SubMenus/Roster.h"
#include "SubMenus/Templates.h"
#include "SubMenus/Actions.h"
#include "SubMenus/Battle.h"

#include "FileHandler.h"



using namespace std;

class InputHandler {

public:
    InputHandler();
    ~InputHandler();

    IndividualCreator* individual_creator;
    TemplateCreator* template_creator;
    ActionCreator* action_creator;


    DataHandler<Investigator>* DHInvestigators;
    DataHandler<Person>* DHPersons;
    DataHandler<Creature>* DHCreatures;
    DataHandler<EldritchHorror>* DHEldritchHorrors;
    DataHandler<Species>* DHSpecies;
    DataHandler<Role>* DHRoles;
    DataHandler<Offensive>* DHOffensives;
    DataHandler<Defensive>* DHDefensives;

    void main_menu();

private:

    Payload* payload;
    map<string, int>* species_map;
    FileHandler* file_handler;
};


#endif //FORC_PA_5_INPUTHANDLER_H
