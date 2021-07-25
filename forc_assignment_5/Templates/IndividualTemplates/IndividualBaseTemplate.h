#ifndef FORC_PA_5_INDIVIDUALBASETEMPLATE_H
#define FORC_PA_5_INDIVIDUALBASETEMPLATE_H

#include <vector>
#include "iostream"
#include "../../Helpers/Structs/Stats.h"
#include "../ActionTemplates/Defensive.h"
#include "../ActionTemplates/Offensive.h"

class IndividualBaseTemplate {
public:
    IndividualBaseTemplate(baseIndividualTemplateStats* base_stats, vector<Offensive*>* offensive_actions, vector<Defensive*>* defensive_actions);
    IndividualBaseTemplate(baseIndividualTemplateStats* base_stats);
//    ~BaseTemplate();

    std::string get_name();
    std::string get_type();

    Range get_life_range();
    Range get_strength_range();
    Range get_intelligence_range();

    void set_life_range(int* life_min, int* life_max);
    void set_strength_range(int* strength_min, int* strength_max);
    void set_intelligence_range(int* intelligence_min, int* intelligence_max);
    void set_name(std::string* name);
    void set_type(std::string* type);
    void add_offensive_action(Offensive* action);
    void add_defensive_action(Defensive* action);
    Offensive* do_offensive_action();
    vector<Offensive*>* get_offensive_actions();
    vector<Defensive*>* get_defensive_actions();

    friend std::ostream& operator<< (std::ostream& out, IndividualBaseTemplate* BaseTemplate);

protected:
    std::string name;
    std::string type = "unchanged from Base_template";
    int life_min;
    int strength_min;
    int intelligence_min;
    int life_max;
    int strength_max;
    int intelligence_max;
    vector<Offensive*>* offensive_actions;
    vector<Defensive*>* defensive_actions;
};
#endif //FORC_PA_5_INDIVIDUALBASETEMPLATE_H
