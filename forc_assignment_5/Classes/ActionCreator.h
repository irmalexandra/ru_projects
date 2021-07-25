#ifndef FORC_PA_5_ACTIONCREATOR_H
#define FORC_PA_5_ACTIONCREATOR_H
#include "../Templates/ActionTemplates/Defensive.h"
#include "../Templates/ActionTemplates/Offensive.h"
#include "../Templates/ActionTemplates/ActionBaseTemplate.h"
#include "../Helpers/HelperFunctions.h"
#include "../Helpers/IndexFinder.h"
#include "../Helpers/Structs/Payload.h"

using namespace std;

class ActionCreator {
public:
    ActionCreator() = default;
    ~ActionCreator() = default;


    Defensive* create_defensive(Payload* payload);
    Offensive* create_offensive(Payload* payload);

private:
    baseActionTemplateStats* get_base_stats(Payload* payload);

};


#endif //FORC_PA_5_ACTIONCREATOR_H
