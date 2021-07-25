#ifndef FORC_PA_5_DISPLAYHELPER_H
#define FORC_PA_5_DISPLAYHELPER_H
#include "Structs/Payload.h"


/*
 *
 * Here are the individuals/Roster display functions
 *
*/
void view_shortened_individuals(Payload* payload);
void view_all_individuals(Payload* payload);
void view_individuals(Payload* payload);
void view_individuals_by_category(Payload* payload);

void view_shortened_offensives(Payload* payload);
void view_shortened_defensives(Payload* payload);
void view_offensives_from_list(vector<Offensive*>* offensives);
void view_defensives_from_list(vector<Defensive*>* defensives);

void view_templates(Payload* payload);
void view_single_template(int species_index, int role_index, Payload* payload);
void view_shortened_templates(Payload* payload);


#endif //FORC_PA_5_DISPLAYHELPER_H
