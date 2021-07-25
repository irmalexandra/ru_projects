#ifndef FORC_PA_4_TEMPLATECREATOR_H
#define FORC_PA_4_TEMPLATECREATOR_H

#include <sstream>
#include "iostream"
#include "istream"
#include "../Templates/Role.h"
#include "../Templates/Species.h"
#include "../Templates/BaseTemplate.h"


using namespace std;

class TemplateCreator {
public:
    TemplateCreator() = default;
    ~TemplateCreator() = default;

    Role* create_role();
    Species* create_species();

private:
    void get_base_stats(baseStats* base_stats = nullptr);
    speciesStats* get_species_stats();
};


#endif //FORC_PA_4_TEMPLATECREATOR_H
