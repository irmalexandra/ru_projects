#ifndef FORC_PA_5_TEMPLATECREATOR_H
#define FORC_PA_5_TEMPLATECREATOR_H

#include <sstream>
#include "limits"
#include "iostream"
#include "istream"
#include "../Templates/IndividualTemplates/Role.h"
#include "../Templates/IndividualTemplates/Species.h"
#include "../Templates/IndividualTemplates/IndividualBaseTemplate.h"
#include "../Helpers/HelperFunctions.h"
#include "../Helpers/Structs/Payload.h"
#include "../Helpers/IndexFinder.h"



using namespace std;

class TemplateCreator {
public:
    TemplateCreator() = default;
    ~TemplateCreator() = default;

    Role* create_role(Payload* payload);
    Species* create_species(Payload* payload);

private:
    void get_base_stats(baseIndividualTemplateStats* base_stats = nullptr, Payload* payload = nullptr);
    speciesStats* get_species_stats(Payload* payload);
};


#endif //FORC_PA_5_TEMPLATECREATOR_H
