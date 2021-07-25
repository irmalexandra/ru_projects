#include "Battle.h"

void battle_menu(Payload* payload, FileHandler* file_handler){
    int choice;
    string filename;
    string folder = "Saves/";

    while(true){
        cout << "1. Start Battle\n2. Load different roster and start battle\n0. Back" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                if(payload->DHPersons->get_data()->empty() && payload->DHInvestigators->get_data()->empty()
                || payload->DHSpecies->get_data()->empty() && payload->DHEldritchHorrors->get_data()->empty() ){
                    cout << "Invalid roster." << endl;
                    break;
                }
                start_battle(payload);
                break;
            case 2:
                cout << "Enter the filename of the roster you want to load: ";
                cin >> filename;
                file_handler->load_roster(payload, &filename);
                if(payload->DHPersons->get_data()->empty() && payload->DHInvestigators->get_data()->empty()
                   || payload->DHSpecies->get_data()->empty() && payload->DHEldritchHorrors->get_data()->empty() ){
                    cout << "Invalid roster." << endl;
                    break;
                }
                start_battle(payload);
                break;
            case 0:
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;
        }
    }
}

void start_battle(Payload* payload){
    auto battle_handler = new BattleHandler(payload);
    battle_handler->start();
}