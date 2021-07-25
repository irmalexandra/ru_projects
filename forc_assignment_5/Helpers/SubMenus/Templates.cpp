#include "Templates.h"


void template_menu(FileHandler* file_handler, TemplateCreator* template_creator, Payload* payload) {
    int choice;
    while(true){
        cout << "1. View templates\n2. Create template\n3. Delete template\n0. Back" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                view_templates(payload);
                break;
            case 2:
                create_template(file_handler, template_creator, payload);
                break;
            case 3:
                delete_template(file_handler, payload);
                break;
            case 0:
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;
        }
    }
}

void create_template(FileHandler* file_handler, TemplateCreator* template_creator, Payload* payload) {
    int choice = 0;

    Species* species;
    Role* role;
    vector<Offensive*> offensives_vector;
    vector<Defensive*> defensive_vector;

    while(true){
        std::cout << "Select:" << std::endl;
        std::cout << "1. Species" << std::endl;
        std::cout << "2. Role" << std::endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                species = template_creator->create_species(payload);
                cout << "Choose offensive action" << endl;
                add_actions(species, payload);
                payload->DHSpecies->get_data()->push_back(species);
                file_handler->save_templates(payload);
                break;
            case 2:
                role = template_creator->create_role(payload);
                add_actions(role, payload);
                payload->DHRoles->get_data()->push_back(role);
                file_handler->save_templates(payload);
                break;
            default:
                cout << "Invalid selection: " << choice << endl;
                break;

        }
        if(!re_prompt()){

            return;
        }
    }
}

void delete_template(FileHandler* file_handler, Payload* payload){
    string name;
    view_shortened_templates(payload);
    cout << "Enter the name of the template you want deleted." << endl;
    cin >> name;

    auto species_index = get_index(payload->DHSpecies->get_data(), name);
    auto role_index = get_index(payload->DHRoles->get_data(), name);
    if(role_index == -1 && species_index == -1){
        cout << name << " does not exist!" << endl;
    }
    else{
        if(species_index != -1){
            payload->DHSpecies->get_data()->erase(payload->DHSpecies->get_data()->begin() + species_index);
            file_handler->save_templates(payload);
        }

        if(role_index != -1){
            payload->DHRoles->get_data()->erase(payload->DHRoles->get_data()->begin() + role_index);
            file_handler->save_templates(payload);
        }
        file_handler->save_templates(payload);
        cout << name << " was successfully deleted!" << endl;
    }
}

