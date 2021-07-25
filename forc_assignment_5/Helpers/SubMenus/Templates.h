#ifndef FORC_PA_5_TEMPLATES_H
#define FORC_PA_5_TEMPLATES_H

#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>

#include "../../Classes/TemplateCreator.h"
#include "../../Templates/IndividualTemplates/Role.h"
#include "../../Templates/IndividualTemplates/Species.h"
#include "../FileHandler.h"
#include "../Structs/Payload.h"
#include "../IndexFinder.h"
#include "../DisplayHelper.h"

using namespace std;

void view_single_template(int species_index, int role_index, Payload* payload);
void template_menu(FileHandler* file_handler, TemplateCreator* template_creator, Payload* payload);
void create_template(FileHandler* file_handler, TemplateCreator* template_creator, Payload* payload);
void view_templates(Payload* payload);
void view_shortened_templates(Payload* payload);
void delete_template(FileHandler* file_handler, Payload* payload);

template <typename T>
void add_actions(T individual_template, Payload* payload){

    int choice;
    bool offensive_added = false;
    bool defensive_added = false;
    bool running = true;
    string action_name;

    // ADDING OFFENSIVE ACTIONS
    while(running){

        cout << "Adding offensive actions to " << individual_template->get_name() << endl << "1. Add action\n0. Done" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        if(!offensive_added && choice == 0){
            cout << "Must add at least one offensive action." << endl;
        }
        else{
            switch (choice) {
                case 1:
                    view_shortened_offensives(payload);
                    cout << "Name: ";
                    cin >> action_name;
                    add_action(payload->DHOffensives->get_data(), individual_template->get_offensive_actions(), action_name);
                    offensive_added = true;
                    break;
                case 0:
                    running = false;
                    break;

                default:
                    cout << "Invalid input" << endl;
                    break;
            }
        }
    }

    running = true;
    // ADDING DEFENSIVE ACTIONS
    while(running){

        cout << "Adding defensive actions to " << individual_template->get_name() << endl << "1. Add action\n0. Done" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }

        if(!defensive_added && choice == 0){
            cout << "Must add at least one defensive action." << endl;
        }
        switch (choice) {
            case 1:
                view_shortened_defensives(payload);
                cout << "Name: ";
                cin >> action_name;
                add_action(payload->DHDefensives->get_data(), individual_template->get_defensive_actions(), action_name);
                defensive_added = true;
                break;
            case 0:
                running = false;
                break;

            default:
                cout << "Invalid input" << endl;
                break;
        }
    }
}

template<typename T>
void add_action(vector<T*>* actions, vector<T*>* template_actions, string name){
    auto index = get_index(actions, name);
    if (index != -1){
        template_actions->push_back(actions->at(index));
        cout << name + " successfully added." << endl;
    } else {
        cout << "Action by the name of " + name + " was not found." << endl;
    }
}

#endif //FORC_PA_5_TEMPLATES_H
