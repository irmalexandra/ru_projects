//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_BEING_H
#define FORC_PA_5_BEING_H


#include "iostream"
#include "../Templates/IndividualTemplates/IndividualBaseTemplate.h"
#include "../Helpers/Structs/Status.h"
#include "../Helpers/Structs/Buffs.h"

using namespace std;

class Being {
public:

    Being(IndividualBaseTemplate* base_template);
    Being(baseIndividualStats* stats);
    Being(baseIndividualStats *stats, IndividualBaseTemplate* base_template);
//    ~Being();

    void edit();

    int get_life();
    int get_strength();
    int get_intelligence();
    int get_initiative();
    std::string get_name();
    bool get_is_investigator();
    battleStats* get_battle_stats();


    void set_name(std::string* name);
    void set_life(int* life);
    void set_strength(int* strength);
    void set_intelligence(int* intelligence);
    void set_is_investigator(bool* is_investigator);
    void roll_initiative();
    void decrease_life(int amount);
    void increase_life(int amount);
    void decrease_fear(int amount);
    void increase_fear(int amount);
    void increase_disquiet(int amount);
    void decrease_disquiet(int amount);

    void take_offensive(Offensive* offensive);
    void apply_buff(Defensive* defensive);
    void update_buffs();


    Status* get_status();


    IndividualBaseTemplate* get_template();

    friend std::ostream& operator<< (std::ostream& out, Being* being);

protected:
    string name;
    battleStats* battle_stats = new battleStats();
    void reset_status();

private:
    Status* status;
    int life;
    int strength;
    int intelligence;
    int initiative;
    bool is_investigator;


    vector<Buff*>* buff_list;
    IndividualBaseTemplate* base_template;
};



#endif //FORC_PA_5_BEING_H
