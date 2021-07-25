#ifndef FORC_PA_5_ROSTER_H
#define FORC_PA_5_ROSTER_H

#include "../FileHandler.h"
#include "../../Classes/IndividualCreator.h"
#include "Templates.h"
#include "../IndexFinder.h"
#include "../DisplayHelper.h"
using namespace std;

void view_individuals_by_category(Payload* payload);
void individual_menu(FileHandler* file_handler, Payload* payload, IndividualCreator* individual_creator);
void select_template_for_individual(FileHandler* file_handler, Payload* payload, IndividualCreator* individual_creator);
void view_shortened_individuals(Payload* payload);
void delete_individual(FileHandler* file_handler, Payload* payload);
void view_individuals(Payload* payload);
void view_all_individuals(Payload* payload);
void auto_save(FileHandler* file_handler, Payload* payload);

#endif //FORC_PA_5_ROSTER_H
