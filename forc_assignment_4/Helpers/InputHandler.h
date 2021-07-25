#ifndef FORC_PA_4_INPUTHANDLER_H
#define FORC_PA_4_INPUTHANDLER_H
#include <vector>
#include <algorithm>
#include <iostream>
#include <fstream>

#include "../Models/Investigator.h"
#include "../Models/Person.h"
#include "../Models/Creature.h"
#include "../Models/EldritchHorror.h"
#include "../Classes/IndividualCreator.h"
#include "../Classes/TemplateCreator.h"
#include "DataHandler.h"

#include "../Templates/Role.h"
#include "../Templates/Species.h"

#include "FileHandler.h"


using namespace std;
class InputHandler {

public:
    InputHandler();
    ~InputHandler();

    IndividualCreator* individual_creator;
    TemplateCreator* template_creator;

    DataHandler<Investigator>* DHInvestigators;
    DataHandler<Person>* DHPersons;
    DataHandler<Creature>* DHCreatures;
    DataHandler<EldritchHorror>* DHEldritchHorrors;
    DataHandler<Species>* DHSpecies;
    DataHandler<Role>* DHRoles;

    void view_individuals_by_category();
    void main_menu();

private:

    void individual_menu();
    void select_template_for_individual();
    void create_individual_species(Species* species);
    void create_individual_role(Role* role);
    void view_individuals();
    void view_all_individuals() const;
    void view_single_template(int species_index, int role_index);
    void auto_save();
    void template_menu();
    void create_template();
    void view_templates();
    void view_shortened_templates();
    void edit_templates();
    void delete_template();
    int get_index_roles(const string& name) const;
    int get_index_species(const string& name) const;


    void view_shortened_individuals();

    Payload* payload;
    map<string, int>* species_map;
    FileHandler* file_handler;
};


#endif //FORC_PA_4_INPUTHANDLER_H
