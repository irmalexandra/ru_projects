#include "Roster.h"


void individual_menu(FileHandler* file_handler, Payload* payload, IndividualCreator* individual_creator) {
    int choice;
    string filename;

    while(true){
        cout << "1. View individuals\n2. Create individual\n3. Delete individual\n4. Save current roster\n5. Load roster\n0. Back" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case(1):
                view_individuals(payload);
                break;
            case(2):
                select_template_for_individual(file_handler, payload, individual_creator);
                break;
            case(3):
                delete_individual(file_handler, payload);
                break;
            case(4):
                cout << "Enter the filename of the new roster: Save/";
                cin >> filename;
                file_handler->save_roster(payload, new string(filename));
                break;
            case(5):
                cout << "Enter the filename of the roster you want to load: ";
                cin >> filename;
                file_handler->load_roster(payload, new string(filename));
                break;
            case(0):
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;
        }
    }
}


void select_template_for_individual(FileHandler* file_handler, Payload* payload, IndividualCreator* individual_creator) {
    view_shortened_templates(payload);
    string name;
    cout << "Enter the name of the template you want to view." << endl;
    cin >> name;
    auto species_index = get_index(payload->DHSpecies->get_data(), name);
    auto role_index = get_index(payload->DHRoles->get_data(), name);
    while(role_index == -1 && species_index == -1) {
        cout << name << " does not exist!" << endl;
        view_shortened_templates(payload);
        cout << "Enter the name of the template you want to view." << endl;
        cin >> name;
        species_index = get_index(payload->DHSpecies->get_data(), name);
        role_index = get_index(payload->DHRoles->get_data(), name);
    }
    view_single_template(species_index, role_index, payload);

    cout << "1. Create Individual based on this template\n0. Back" << endl;
    int choice = 0;
    cin >> choice;
    if(cin.fail()){
        cout << "Invalid input, going back" << endl;
        cin.clear();
        cin.ignore(std::numeric_limits<int>::max(),'\n');
        return;
    }
    switch (choice) {
        case 1:
            if (species_index >= 0) {
                auto species = payload->DHSpecies->get_data()->at(species_index);
                if (species->get_is_eldritch()){
                    auto new_eldritch_horror = individual_creator->createEldritchHorror(species, payload);
                    if(payload->species_map->find(new_eldritch_horror->get_template()->get_name()) == payload->species_map->end()){
                        payload->species_map->insert(std::pair<string, int>(new_eldritch_horror->get_template()->get_name(), 0));
                    }
                    new_eldritch_horror->set_name(new string(new_eldritch_horror->get_name() +
                                                             to_string(++payload->species_map->find(new_eldritch_horror->get_template()->get_name())->second)));

                    new_eldritch_horror->set_is_investigator(new bool(false));
                    payload->DHEldritchHorrors->get_data()->push_back(new_eldritch_horror);
                    cout << new_eldritch_horror << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                    string original_name = new_eldritch_horror->get_name();
                    cin >> choice;
                    while(cin.fail()){
                        cout << "Invalid input" << endl;
                        cin.clear();
                        cin.ignore(std::numeric_limits<int>::max(),'\n');
                        cout << new_eldritch_horror << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                        cin >> choice;
                    }
                    if(choice == 1){
                        new_eldritch_horror->edit(payload->DHEldritchHorrors->get_data());
                        if(original_name != new_eldritch_horror->get_name()){
                            payload->species_map->find(new_eldritch_horror->get_template()->get_name())->second--;
                        }
                    }
                    auto_save(file_handler, payload);
                }
                else {
                    auto new_creature = individual_creator->createCreature(species, payload);
                    new_creature->set_is_investigator(new bool(false));
                    if(payload->species_map->find(new_creature->get_template()->get_name()) == payload->species_map->end()){
                        payload->species_map->insert(std::pair<string, int>(new_creature->get_template()->get_name(), 0));
                    }
                    new_creature->set_name(new string(new_creature->get_name()
                                                      + to_string(++payload->species_map->find(new_creature->get_template()->get_name())->second)));
                    payload->DHCreatures->get_data()->push_back(new_creature);
                    cout << new_creature << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                    string original_name = new_creature->get_name();
                    cin >> choice;
                    while(cin.fail()){
                        cout << "Invalid input" << endl;
                        cin.clear();
                        cin.ignore(std::numeric_limits<int>::max(),'\n');
                        cout << new_creature << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                        cin >> choice;
                    }
                    if(choice == 1){
                        new_creature->edit(payload->DHCreatures->get_data());
                        if(original_name != new_creature->get_name()){
                            payload->species_map->find(new_creature->get_template()->get_name())->second--;
                        }
                    }
                    auto_save(file_handler, payload);
                }

            } else if (role_index >= 0) {
                auto role = payload->DHRoles->get_data()->at(role_index);
                cout << "1. Investigator (playable character)\n2. Person (NPC)\n0. Return" << endl;
                bool runner = true;
                cin >> choice;
                while(cin.fail()){
                    cout << "Invalid input" << endl;
                    cin.clear();
                    cin.ignore(std::numeric_limits<int>::max(),'\n');
                    cout << "1. Investigator (playable character)\n2. Person (NPC)\n3. Return" << endl;
                    cin >> choice;
                }
                while(runner){
                    if (choice == 1){
                        auto new_investigator = individual_creator->createInvestigator(role, payload);
                        new_investigator->set_is_investigator(new bool(true));
                        payload->DHInvestigators->get_data()->push_back(new_investigator);
                        cout << new_investigator << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                        cin >> choice;
                        while(cin.fail()){
                            cout << "Invalid input" << endl;
                            cin.clear();
                            cin.ignore(std::numeric_limits<int>::max(),'\n');
                            cout << new_investigator << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                            cin >> choice;
                        }
                        if(choice == 1){
                            new_investigator->edit(payload->DHInvestigators->get_data());
                        }
                        runner = false;
                        auto_save(file_handler, payload);

                    }
                    else if (choice == 2){
                        auto new_person = individual_creator->createPerson(role, payload);
                        new_person->set_is_investigator(new bool(false));
                        payload->DHPersons->get_data()->push_back(new_person);
                        cout << new_person << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                        cin >> choice;
                        while(cin.fail()){
                            cout << "Invalid input" << endl;
                            cin.clear();
                            cin.ignore(std::numeric_limits<int>::max(),'\n');
                            cout << new_person << endl << "Do you wish to edit this individual?\n1. yes\n2. no" << endl;
                            cin >> choice;
                        }
                        if(choice == 1){
                            new_person->edit(payload->DHPersons->get_data());
                        }

                        runner = false;
                        auto_save(file_handler, payload);
                    }
                    else{
                        cout << choice << " is not a valid option" << endl;
                        cout << "1. Investigator (playable character)\n2. Person (NPC)\n0. Return" << endl;
                        cin >> choice;
                        while(cin.fail()){
                            cout << "Invalid input" << endl;
                            cin.clear();
                            cin.ignore(std::numeric_limits<int>::max(),'\n');
                            cout << "1. Investigator (playable character)\n2. Person (NPC)\n0. Return" << endl;
                            cin >> choice;
                        }
                    }
                }

            }
        case 0:
            return;
    }
}

void delete_individual(FileHandler* file_handler, Payload* payload){
    string name;
    view_shortened_individuals(payload);
    cout << "Enter the name of the individual you want deleted." << endl;
    cin >> name;

    auto creature_index = get_index(payload->DHCreatures->get_data(), name);
    auto eldritch_horror_index = get_index(payload->DHEldritchHorrors->get_data(), name);
    auto person_index = get_index(payload->DHPersons->get_data(), name);
    auto investigator_index = get_index(payload->DHInvestigators->get_data(), name);

    if(creature_index + eldritch_horror_index + person_index + investigator_index == -4){ // indexes are all -1 if an individual was not found
        cout << name << " does not exist!" << endl;
    }
    else{
        if(creature_index != -1){
            payload->DHCreatures->get_data()->erase(payload->DHCreatures->get_data()->begin() + creature_index);
            file_handler->save_templates(payload);
        }

        if(eldritch_horror_index != -1){
            payload->DHEldritchHorrors->get_data()->erase(payload->DHEldritchHorrors->get_data()->begin() + eldritch_horror_index);
            file_handler->save_templates(payload);
        }
        if(person_index != -1){
            payload->DHPersons->get_data()->erase(payload->DHPersons->get_data()->begin() + person_index);
            file_handler->save_templates(payload);
        }
        if(investigator_index != -1){
            payload->DHInvestigators->get_data()->erase(payload->DHInvestigators->get_data()->begin() + investigator_index);
            file_handler->save_templates(payload);
        }
        cout << name << " was successfully deleted!" << endl;
    }
}

void auto_save(FileHandler* file_handler, Payload* payload){
    cout << "Auto Saving..." << endl;
    file_handler->save_roster(payload, new string("backups/roster_backup"));
}
