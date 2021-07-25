#include "Actions.h"


void action_menu(FileHandler* file_handler, ActionCreator* action_creator, Payload* payload){
    int choice;
    while(true){
        cout << "1. View actions\n2. Create actions\n0. Back" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                view_actions(payload);
                break;
            case 2:
                create_action(file_handler, action_creator, payload);
                break;
            case 0:
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;
        }
    }
};

void view_actions(Payload* payload){
    cout << "Offensive actions" << endl;
    cout << payload->DHOffensives << endl;
    cout << "\nDefensive actions" << endl;
    cout << payload->DHDefensives << endl;
};



void create_action(FileHandler* file_handler, ActionCreator* action_creator, Payload* payload){
    int choice = 0;

    Offensive* offensive;
    Defensive* defensive;


    while(true){
        std::cout << "Select:" << std::endl;
        std::cout << "1. Offensive action" << std::endl;
        std::cout << "2. Defensive action" << std::endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                offensive = action_creator->create_offensive(payload);
                payload->DHOffensives->get_data()->push_back(offensive);
                file_handler->save_actions(payload);
                break;
            case 2:
                defensive = action_creator->create_defensive(payload);
                payload->DHDefensives->get_data()->push_back(defensive);
                file_handler->save_actions(payload);
                break;
            default:
                cout << "Invalid selection: " << choice << endl;
                break;

        }
        if(!re_prompt()){

            return;
        }
    }
};



