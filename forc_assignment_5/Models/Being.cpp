//
// Created by emmik on 22/03/2021.
//

#include "Being.h"

#include "../Helpers/HelperFunctions.h"
#include "iostream"

Being::Being(IndividualBaseTemplate* base_template){
    this->life = get_random_integer(base_template->get_life_range());
    this->strength = get_random_integer(base_template->get_strength_range());
    this->intelligence = get_random_integer(base_template->get_intelligence_range());

    this->base_template = base_template;
    this->status = new Status();
    this->buff_list = new vector<Buff*>;
}

Being::Being(baseIndividualStats *stats) {
    this->life = stats->life;
    this->strength = stats->strength;
    this->intelligence = stats->intelligence;
    this->is_investigator = false;
    this->buff_list = new vector<Buff*>;

    this->status = new Status();
}

Being::Being(baseIndividualStats *stats, IndividualBaseTemplate* base_template) {
    this->life = stats->life;
    this->strength = stats->strength;
    this->intelligence = stats->intelligence;
    this->is_investigator = false;
    this->buff_list = new vector<Buff*>;

    this->base_template = base_template;

    this->status = new Status();
}

battleStats* Being::get_battle_stats(){
    return this->battle_stats;
}

void Being::set_name(std::string* name){
    this->name = *name;

    delete name;
    name = nullptr;
}

IndividualBaseTemplate* Being::get_template() {
    return this->base_template;
}

std::string Being::get_name() {
    return this->name;
}

int Being::get_life() {
    return this->life;
}

int Being::get_intelligence() {
    return this->intelligence;
}

int Being::get_strength() {
    return this->strength;
}

void Being::set_life(int* life) {
    this->life = *life;

    delete life;
    life = nullptr;
}

void Being::set_intelligence(int* intelligence) {
    this->intelligence = *intelligence;

    delete intelligence;
    intelligence = nullptr;
}

void Being::set_strength(int* strength) {
    this->strength = *strength;

    delete strength;
    strength = nullptr;
}

bool Being::get_is_investigator() {
    return this->is_investigator;
}

void Being::set_is_investigator(bool *is_investigator) {
    this->is_investigator = *is_investigator;

    delete is_investigator;
    is_investigator = nullptr;
}

std::ostream& operator<< (std::ostream& out, Being* being) {
    out << "Name: " << being->get_name() << std::endl;
    if (being->get_is_investigator()){
        out << "Investigator" << std::endl;
    }else{
        out << being->get_template()->get_type() << std::endl;
    }
    out << being->get_template()->get_name() << std::endl;
    out << "Life: " << being->get_life() << std::endl;
    out << "Strength: " << being->get_strength() << std::endl;
    out << "Intelligence: " << being->get_intelligence() << std::endl;

    return out;
}

void Being::roll_initiative() {
    this->initiative = get_random_integer(Range(1, 20));
    cout << this->get_name() << " rolled an initiative value of: " << this->get_initiative() << endl;
}

int Being::get_initiative() {
    return this->initiative;
}

Status* Being::get_status() {
    return this->status;
}

void Being::decrease_life(int amount) {
    this->get_battle_stats()->current_life -= amount;
    if (this->get_battle_stats()->current_life <= 0){
        this->get_battle_stats()->current_life = 0;
        this->status->dead = true;
    }
}

void Being::increase_life(int amount) {
    this->get_battle_stats()->current_life += amount;
    if (this->get_battle_stats()->current_life > this->get_battle_stats()->life){
        this->get_battle_stats()->current_life = this->get_battle_stats()->life;
    }
    this->status->dead = false;
}

void Being::decrease_fear(int amount) {
    this->get_battle_stats()->current_fear -= amount;
    if (this->get_battle_stats()->current_fear <= 0){
        this->get_battle_stats()->current_fear = 0;
    }
}

void Being::increase_fear(int amount) {
    this->get_battle_stats()->current_fear += amount;
    if (this->get_battle_stats()->current_fear > this->get_battle_stats()->fear){
        this->get_battle_stats()->current_fear = this->get_battle_stats()->fear;
    }

}
void Being::increase_disquiet(int amount) {
    this->get_battle_stats()->current_disquiet += amount;
    if (this->get_battle_stats()->current_disquiet > this->get_battle_stats()->disquiet){
        this->get_battle_stats()->current_disquiet = this->get_battle_stats()->disquiet;
    }
}

void Being::decrease_disquiet(int amount) {
    this->get_battle_stats()->current_disquiet -= amount;
    if (this->get_battle_stats()->current_disquiet <= 0){
        this->get_battle_stats()->current_disquiet = 0;
        this->status->overcame = true;
    }
}

void Being::take_offensive(Offensive* offensive) {
    if (!offensive->is_physical()) {
        if (this->get_template()->get_type() == "Person" || this->get_template()->get_type() == "Investigator") {
            this->increase_fear(offensive->get_damage());
            cout << offensive->get_name() << " increases " << this->get_name() << "'s fear by "
                 << offensive->get_damage() << "." << endl;
            cout << this->get_name() << "'s fear is now " << this->get_battle_stats()->current_fear << " out of "
                 << this->get_battle_stats()->fear << endl;
        } else {
            this->decrease_disquiet(offensive->get_damage());
            cout << offensive->get_name() << " decreases " << this->get_name() << "'s disquiet by "
                 << offensive->get_damage() << "." << endl;
            cout << this->get_name() << "'s disquiet is now " << this->get_battle_stats()->current_disquiet
                 << " out of " << this->get_battle_stats()->disquiet << endl;
        }

    } else {
        decrease_life(offensive->get_damage());
        cout << offensive->get_name() << " damages " << this->get_name() << " for " << offensive->get_damage() << " life." << endl;
        cout << this->get_name() << "'s life is now " << this->get_battle_stats()->current_life << endl;
    }
}

void Being::apply_buff(Defensive* defensive) {
    this->buff_list->push_back(new Buff(defensive));
    if (defensive->get_def_modifier() > 0){
        if (defensive->is_physical()){
            this->get_battle_stats()->physical_defense_modifier += defensive->get_def_modifier();
            cout << this->get_name() << "'s defensive strength has increased by " << defensive->get_def_modifier()
                 << " and is now " << this->get_battle_stats()->get_strength_defense() << "." << endl;
        } else {
            this->get_battle_stats()->mental_defense_modifier += defensive->get_def_modifier();
            cout << this->get_name() << "'s defensive intelligence has increased by " << defensive->get_def_modifier()
                 << " and is now " << this->get_battle_stats()->get_intelligence_defense() << "." << endl;
        }
    }

    if (defensive->get_atk_modifier() > 0){
        if (defensive->is_physical()){
            this->get_battle_stats()->physical_attack_modifier += defensive->get_atk_modifier();
            cout << this->get_name() << "'s offensive strength has increased by " << defensive->get_atk_modifier()
                 << " and is now " << this->get_battle_stats()->get_intelligence_defense() << "." << endl;
        } else {
            this->get_battle_stats()->mental_attack_modifier += defensive->get_atk_modifier();
            cout << this->get_name() << "'s offensive intelligence has increased by " << defensive->get_atk_modifier()
                 << " and is now " << this->get_battle_stats()->get_intelligence_defense() << "." << endl;
        }
    }

    if (defensive->get_recovery() > 0){
        if (!defensive->is_physical() && this->get_template()->get_type() == "Role"){
            this->get_battle_stats()->current_fear -= defensive->get_recovery();
            if (this->get_battle_stats()->current_fear < 0){
                this->get_battle_stats()->current_fear = 0;
            }
            cout << defensive->get_name() << " has recovered "<< this->get_name() << "'s fear by " << defensive->get_recovery() << "." << endl;

        } else {
            this->increase_life(defensive->get_recovery());
            if (defensive->get_recovery() > 0){
                cout << defensive->get_name() << " has healed "<< this->get_name() << " for " << defensive->get_recovery() << " life." << endl;
                cout << this->get_name() << " now has " << this->get_battle_stats()->current_life << " life." << endl;
            }
        }
    }
    cout << defensive->get_name() << " has been applied and will last for " << defensive->get_duration() << " rounds." << endl;
}

void Being::update_buffs() {
    Buff* buff;
    for(int i = 0; i < this->buff_list->size(); i++){
        buff = this->buff_list->at(i);
        buff->duration_remaining -= 1;
        if(this->buff_list->at(i)->duration_remaining <= 0){
            this->buff_list->erase(this->buff_list->begin() + i);
            if (buff->action->is_physical()){
                this->get_battle_stats()->physical_defense_modifier -= buff->action->get_def_modifier();
                this->get_battle_stats()->physical_attack_modifier -= buff->action->get_atk_modifier();
            } else {
                this->get_battle_stats()->mental_defense_modifier -= buff->action->get_def_modifier();
                this->get_battle_stats()->mental_attack_modifier -= buff->action->get_atk_modifier();
            }
            cout << buff->action->get_name() << " has run out on " << this->get_name() << endl;
            continue;
        }
        else{
            cout << buff->action->get_name() << " is still in effect on " << this->get_name() << " with " << buff->duration_remaining << " rounds remaining." << endl;
        }

        if (buff->action->get_recovery() != 0){
            if (!buff->action->is_physical() && this->get_template()->get_type() == "Role"){
                this->get_battle_stats()->current_fear -= buff->action->get_recovery();
                if (this->get_battle_stats()->current_fear < 0){
                    this->get_battle_stats()->current_fear = 0;
                }
                cout << buff->action->get_name() << " has recovered "<< this->get_name() << "'s fear by " << buff->action->get_recovery() << "." << endl;
                cout << this->get_name() << " now has " << this->get_battle_stats()->current_fear << " out of " << this->get_battle_stats()->fear << " fear." << endl;
            } else {
                this->increase_life(buff->action->get_recovery());
                cout << buff->action->get_name() << " has healed "<< this->get_name() << " for " << buff->action->get_recovery() << " life." << endl;
                cout << this->get_name() << " now has " << this->get_battle_stats()->current_life << " life." << endl;
            }
        }

    }
}

void Being::reset_status() {
    this->status = new Status();
}




