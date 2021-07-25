#include "BattleHandler.h"


BattleHandler::BattleHandler(Payload* payload) {
    this->payload = payload;
    cout << "Launching battle simulator." << endl;
    cout << "Rolling initiative." << endl;

    this->participant_list = new vector<Being*>;
    for (auto & creature : *payload->DHCreatures->get_data()){
        creature->roll_initiative();
        creature->reset();
        this->participant_list->push_back((Being*)creature);
        this->monster_team_count++;
    }
    for (auto & investigator : *payload->DHInvestigators->get_data()){
        investigator->roll_initiative();
        investigator->reset();
        this->participant_list->push_back((Being*)investigator);
        this->investigator_team_count++;
    }
    for (auto & person : *payload->DHPersons->get_data()){
        person->roll_initiative();
        person->reset();
        this->participant_list->push_back((Being*)person);
        this->investigator_team_count++;
    }
    for (auto & horror : *payload->DHEldritchHorrors->get_data()){
        horror->roll_initiative();
        horror->reset();
        this->participant_list->push_back((Being*)horror);
        this->monster_team_count++;
    }
    set_turn_order();
    cout << "Turn order established." << endl;
    this->turn_size = this->participant_list->size();
}

bool compare (Being* lb, Being* rb){
    return lb->get_initiative() > rb->get_initiative();
}

void BattleHandler::set_turn_order() {
    sort(this->participant_list->begin(), this->participant_list->end(), compare);
}

void BattleHandler::start() {
    while (monster_team_count > 0 && investigator_team_count > 0){
        cout << "========================= Starting round: " << this->round_tracker+1 << " =========================\n" << endl;
        while (this->turn_tracker < this->turn_size){
            if (monster_team_count == 0 || investigator_team_count == 0){
                this->turn_tracker++;
                continue;
            }
            if(this->participant_list->at(this->turn_tracker)->get_status()->dead || this->participant_list->at(this->turn_tracker)->get_status()->fleeing ||
                                                                                     this->participant_list->at(this->turn_tracker)->get_status()->overcame) {
                this->turn_tracker++;
                continue;
            }
            cout << "----------------------- STATUS UPDATE ---------------------" << endl;
            set_status();
            cout << "-----------------------------------------------------------\n" << endl;
            cout << this->participant_list->at(this->turn_tracker)->get_name() << "'s turn begins." << endl;

            this->participant_list->at(this->turn_tracker)->update_buffs();
            if(this->participant_list->at(this->turn_tracker)->get_is_investigator()){
                execute_ai_turn();
            } else {
                execute_ai_turn();
            }
            cout << this->participant_list->at(this->turn_tracker)->get_name() << "'s turn ends." << endl;
            this->turn_tracker++;
            cout << endl;
        }
        this->decrement_cooldowns();
        this->turn_tracker = 0;
        this->round_tracker++;
    }
    cout << "============================ Battle over ============================" << endl;
    if (monster_team_count < investigator_team_count){
        cout << "Humans persevere!\n" << endl;
    } else {
        cout << "All is lost!\n" << endl;
    }
}

string BattleHandler::find_target() {
    auto current_participant = this->participant_list->at(this->turn_tracker);
    auto type = current_participant->get_template()->get_type();
    auto targets = new vector<string>;
    for(auto target : *this->participant_list){
        if (current_participant->get_name() == target->get_name()){continue;}
        if (type == "Person" || type == "Investigator"){
            if (target->get_template()->get_type() == "Person" || target->get_template()->get_type() == "Investigator"){continue;}
            if(!target->get_status()->dead && !target->get_status()->fleeing && !target->get_status()->overcame){
                targets->push_back(target->get_name());
                if(target->get_status()->injured){
                    targets->push_back(target->get_name());
                }
                if(target->get_status()->insane){
                    targets->push_back(target->get_name());
                }
            }
        }else{
            if (target->get_template()->get_type() == "Creature" || target->get_template()->get_type() == "Eldritch Horror"){continue;}
            if(!target->get_status()->dead && !target->get_status()->fleeing && !target->get_status()->overcame){
                targets->push_back(target->get_name());
                if(target->get_status()->injured){
                    targets->push_back(target->get_name());
                }
                if(target->get_status()->insane){
                    targets->push_back(target->get_name());
                }
            }
        }

    }
    auto random = get_random_integer(Range(0, targets->size()));
    return targets->at(random);
}

string BattleHandler::find_action() {
    auto actions = new vector<string>;
    auto current_participant = this->participant_list->at(this->turn_tracker);
    auto type = current_participant->get_template()->get_type();
    auto off_actions = current_participant->get_template()->get_offensive_actions();
    auto def_actions = current_participant->get_template()->get_defensive_actions();

    for (auto o_action : *off_actions){
        actions->push_back(o_action->get_name());
        if (!current_participant->get_status()->injured){
            actions->push_back(o_action->get_name());
        }
        if (!current_participant->get_status()->insane){
            actions->push_back(o_action->get_name());
        }

        if (!current_participant->get_status()->frightened){
            actions->push_back(o_action->get_name());
        }

        if (!current_participant->get_status()->outnumbered){
            actions->push_back(o_action->get_name());
        } else{

        }

        if (type == "Eldritch Horror"){
            if (!current_participant->get_status()->enraged){
                actions->push_back(o_action->get_name());
            }
        }
    }

    for (auto d_action : *def_actions){
        actions->push_back(d_action->get_name());
        if (current_participant->get_status()->injured){
            actions->push_back(d_action->get_name());
        }
        if (current_participant->get_status()->insane){
            actions->push_back(d_action->get_name());
            auto person = (Person*)current_participant;
            for (int i = 0; i < person->get_battle_stats()->current_fear; i++){
                actions->push_back("Meltdown");
            }
        }

        if (current_participant->get_status()->frightened){
            actions->push_back(d_action->get_name());
            if (type == "Person" || type == "Investigator"){
                auto person = (Person*)current_participant;
                for (int i = 0; i < person->get_battle_stats()->current_fear; i++){
                    actions->push_back("Flee");
                }
            }
            else{
                for (int i = 0; i < (this->investigator_team_count-this->monster_team_count); i++){
                    actions->push_back("Flee");
                }
            }
        }

        if (current_participant->get_status()->outnumbered){
            actions->push_back(d_action->get_name());
        }
    }
    return actions->at(get_random_integer(Range(0, actions->size())));
}

void BattleHandler::execute_ai_turn() {
    bool use_offensive = false;
    bool use_defensive = false;

    auto participant = this->participant_list->at(this->turn_tracker);

    auto action = find_action();

    if (action == "Flee"){
        cout << participant->get_name() << " is overcome by fear and flees the battle!" << endl;
        if (participant->get_template()->get_type() == "Person"){
            this->investigator_team_count--;
        } else {
            this->monster_team_count--;
        }
        participant->get_status()->set_fleeing(true);

        return;
    }
    if (action == "Meltdown"){
        cout << participant->get_name() << " has been driven insane and was unable to overcome his status for this round!" << endl;
        return;
    }

    for (auto o_action : *participant->get_template()->get_offensive_actions()){
        if (o_action->get_name() == action){
            use_offensive = true;
        }
    }
    if (!use_offensive){
        for (auto d_action : *participant->get_template()->get_defensive_actions()){
            if (d_action->get_name() == action){
                use_defensive = true;
            }
        }
    }

    if (use_offensive){
        auto target_name = find_target();
        this->execute_offensive_action(participant, target_name, action);

    }
    if (use_defensive){
        this->execute_defensive_action(participant, action);
    }
}

void BattleHandler::execute_offensive_action(Being* participant, string target_name, string action_name) {
    Being* target;
    for(auto being : *this->participant_list){
        if(being->get_name() == target_name){
            target = being;
            break;
        }
    }
    Offensive* offensive_action = participant->get_template()->get_offensive_actions()->at(get_index(participant->get_template()->get_offensive_actions(), action_name));

    int participant_result = get_random_integer(Range(1, 20));
    int target_result = get_random_integer(Range(1, 20));

    cout << participant->get_name() << " attempts to use " << action_name << " on " << target_name << "!" << endl;
    if(offensive_action->is_physical()){
        cout << participant->get_name() << " rolls " << participant_result;
        participant_result += participant->get_battle_stats()->get_strength_attack();
        cout << " plus " << participant->get_name() << "'s strength: " << to_string(participant->get_battle_stats()->get_strength_attack())
             << " which results in " << participant_result << endl;

        cout << target_name << " rolls " << target_result;
        target_result += target->get_battle_stats()->get_strength_defense();
        cout << " plus " << target_name << "'s strength: " << to_string(target->get_battle_stats()->get_strength_defense())
             << " which results in " << target_result << endl;

        if(participant_result > target_result){
            cout << participant->get_name() << "'s attack was a success!" << endl;
            target->take_offensive(offensive_action);
            if (target->get_status()->dead){
                if (target->get_template()->get_type() == "Person"){
                    this->investigator_team_count--;
                } else {
                    this->monster_team_count--;
                }
                cout << target_name << " is now dead." << endl;
            }
        }
        else{
            cout << participant->get_name() << " missed!" << endl;
        }
    }
    else{
        cout << participant->get_name() << " rolls " << participant_result;
        participant_result += participant->get_battle_stats()->get_intelligence_attack();
        cout << " plus " << participant->get_name() << "'s intelligence: " << to_string(participant->get_battle_stats()->get_intelligence_attack())
             << " which results in " << participant_result << endl;

        cout << target_name << " rolls " << target_result;
        target_result += target->get_battle_stats()->get_intelligence_defense();
        cout << " plus " << target_name << "'s intelligence: " << to_string(target->get_battle_stats()->get_intelligence_defense())
             << " which results in " << target_result << endl;

        if(participant_result > target_result){
            cout << participant->get_name() << "'s " << action_name << " was a success!" << endl;
            target->take_offensive(offensive_action);

            if (target->get_status()->overcame){
                this->monster_team_count--;
                cout << "The human's have overcome " << target_name << endl;
                cout << target_name << " is no longer a threat" << endl;
            }
        }
        else{
            cout << participant->get_name() << "'s attack failed!" << endl;
        }
    }
}

void BattleHandler::execute_defensive_action(Being* participant, string action_name) {
    Defensive* defensive_action = participant->get_template()->get_defensive_actions()->at(get_index(participant->get_template()->get_defensive_actions(), action_name));
    cout << participant->get_name() << " casts " << action_name << " on self." << endl;
    participant->apply_buff(defensive_action);
}

void BattleHandler::set_status() {
    for (auto participant : *this->participant_list){
        auto type = participant->get_template()->get_type();

        if (type == "Creature" || type == "Eldritch Horror"){
            if (participant->get_battle_stats()->current_disquiet <= 0){
                participant->get_status()->set_overcame(true);
                continue;
            }
        }

        // Checking if participant is dead, if true, ignores rest of status checks
        if(participant->get_battle_stats()->current_life <= 0){
            participant->get_status()->set_dead(true);
            continue;
        }
        if(participant->get_status()->fleeing){
            continue;
        }

        //  Checking if participant is injured
        if(participant->get_battle_stats()->current_life <= participant->get_life()/2){
            if (!participant->get_status()->injured){
                participant->get_status()->set_injured(true);
                cout << participant->get_name() << " has been injured!" << endl;
            } else {
                cout << participant->get_name() << " is still injured!" << endl;
            }
        }else{
            participant->get_status()->set_injured(false);
        }

        // Checking if participant feels outnumbered
        if(type == "Person" || type == "Investigator"){
            if(float((((float)this->monster_team_count / (float)this->investigator_team_count) - 1)) > 0.3){
                participant->get_status()->set_outnumbered(true);
                cout << participant->get_name() << " is outnumbered!" << endl;
            }
            else{
                participant->get_status()->set_outnumbered(false);
            }
        }
        else{
            if(float((((float)this->investigator_team_count / (float)this->monster_team_count) - 1)) > 0.3){
                participant->get_status()->set_outnumbered(true);
                cout << participant->get_name() << " is outnumbered!" << endl;
            }
            else{
                participant->get_status()->set_outnumbered(false);
            }
        }

        // Checking if participant is frightened
        if (type != "Eldritch Horror"){
            if(type == "Person" || type == "Investigator"){
                auto person = (Person*) participant;
                if(person->get_battle_stats()->current_fear >= person->get_fear()/2){
                    person->get_status()->set_frightened(true);
                    cout << participant->get_name() << " is frightened!" << endl;
                }else{
                    person->get_status()->set_frightened(false);
                }
                if(person->get_battle_stats()->current_fear >= person->get_fear()){
                    person->get_status()->set_insane(true);
                    cout << participant->get_name() << " has been driven insane!" << endl;
                }
                else{
                    person->get_status()->set_insane(false);
                }
            }else{
                if (type == "Creature"){
                    auto creature = (Creature*) participant;
                    if(!creature->get_unnatural()){
                        if (participant->get_status()->injured && participant->get_status()->outnumbered){
                            participant->get_status()->set_frightened(true);
                            cout << participant->get_name() << " is frightened!" << endl;
                        }else{
                            participant->get_status()->set_frightened(false);
                        }
                    }

                }
            }
        }
    }
}

void BattleHandler::decrement_cooldowns() {
    for (auto participant : *this->participant_list){
        for (auto action : *participant->get_template()->get_offensive_actions()){
            action->decrement_cooldown_remaining();
        }
        for (auto action : *participant->get_template()->get_defensive_actions()){
            action->decrement_cooldown_remaining();
        }
    }
}


