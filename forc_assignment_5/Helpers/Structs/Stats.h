#ifndef FORC_PA_5_STATS_H
#define FORC_PA_5_STATS_H

#include <iostream>
#include "vector"

struct baseIndividualTemplateStats {
    std::string name = "";
    std::string type= "unchanged from base_Stats";
    int life_min = 0;
    int life_max = 0;
    int str_min = 0;
    int str_max = 0;
    int int_min = 0;
    int int_max = 0;
};

struct baseIndividualStats{

    std::string name = "";
    std::string type = "unchanged from base_Stats";
    std::string gender = "";
    int life = 0;
    int strength= 0;
    int intelligence= 0;
    bool unnatural = false;
    int disquiet = 0;
    int fear = 0;
    int traumatism = 0;
    int terror = 0;
};

struct battleStats {
    void set_base_stats(int life, int strength, int intelligence){
        this->life = life;
        this->strength = strength;
        this->intelligence = intelligence;
        this->current_life = life;
    };

    void set_as_person(int life, int strength, int intelligence, int fear){
        set_base_stats(life, strength, intelligence);
        this->fear = fear;
    };

    void set_as_creature(int life, int strength, int intelligence, int disquiet){
        set_base_stats(life, strength, intelligence);
        this->disquiet = disquiet;
        this->current_disquiet = disquiet;
    };

    int get_strength_defense(){
        return this->strength + this->physical_defense_modifier;
    };
    int get_intelligence_defense(){
        return this->intelligence + this->mental_defense_modifier;
    };
    int get_strength_attack(){
        return this->strength + this->physical_attack_modifier;
    };
    int get_intelligence_attack(){
        return this->intelligence + this->mental_attack_modifier;
    };


public:
    int life = 0;
    int strength = 0;
    int intelligence = 0;
    int disquiet = 0;
    int current_disquiet = 0;
    int fear = 0;
    int current_fear = 0;
    int current_life = 0;

    int mental_defense_modifier = 0;
    int mental_attack_modifier = 0;
    int physical_defense_modifier = 0;
    int physical_attack_modifier = 0;
};

struct baseActionTemplateStats{
    std::string name = "";
    std::string type = "";
    bool physical = true;
    int cooldown;
};

struct Range {
    Range(){
    }

    Range(int min, int max);

    friend std::ostream& operator<< (std::ostream& out, Range range);

    int min = 0;
    int max = 0;
};

#endif //FORC_PA_5_STATS_H
