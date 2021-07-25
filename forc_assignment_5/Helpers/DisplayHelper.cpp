#include "DisplayHelper.h"

/* ----Individuals / Roster ---- */

void view_shortened_individuals(Payload* payload){
    cout << "Individuals:" << endl << endl << "Persons(NPCs)" << endl;

    for(const auto individual: *payload->DHPersons->get_data()){
        cout << '\t' << individual->get_name() << endl;
    }
    cout << "Investigators:" << endl;
    for(const auto individual: *payload->DHInvestigators->get_data()){
        cout << '\t' << individual->get_name() << endl;
    }
    cout << "Creatures " << endl;
    for(const auto individual: *payload->DHCreatures->get_data()){
        cout << '\t' << individual->get_name() << endl;
    }
    cout << "Eldritch Horrors" << endl;
    for(const auto individual: *payload->DHEldritchHorrors->get_data()){
        cout << '\t' << individual->get_name() << endl;
    }
}

void view_all_individuals(Payload* payload) {
    cout << "Created individuals\n" << endl;
    cout << "Investigators:" << endl;
    cout << payload->DHInvestigators << endl;
    cout << "\nNPCs:" << endl;
    cout << payload->DHPersons << endl;
    cout << "\nCreatures:" << endl;
    cout << payload->DHCreatures << endl;
    cout << "\nEldritch Horrors" << endl;
    cout << payload->DHEldritchHorrors << endl;
}

void view_individuals(Payload* payload) {
    int choice;
    while(true){
        cout << "1. View all individuals\n2. View by category\n0. Back" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case(1):
                view_all_individuals(payload);
                break;
            case(2):
                view_individuals_by_category(payload);
                break;
            case(0):
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;

        }
    }
}

void view_individuals_by_category(Payload* payload) {

    int choice;

    while(true){
        cout << "Select a character type" << std::endl;
        cout << "1. Investigator" << endl;
        cout << "2. Person (NPC)" << endl;
        cout << "3. Creature" << endl;
        cout << "4. Eldritch Horror" << endl;
        cout << "0. Back" << std::endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch(choice){
            case 1:
                cout << payload->DHInvestigators << endl;
                break;
            case 2:
                cout << payload->DHPersons << endl;
                break;
            case 3:
                cout << payload->DHCreatures << endl;
                break;
            case 4:
                cout << payload->DHEldritchHorrors << endl;
                break;
            case 0:
                return;
            default:
                cout<<"Error"<<endl;
                break;

        }
    }
}

 /* ----Actions ----*/

void view_shortened_offensives(Payload* payload){
    cout << "Offensive actions" << endl << endl;
    for(auto action: *payload->DHOffensives->get_data()){
        cout << action->get_name() << endl;
    }

}

void view_shortened_defensives(Payload* payload){
    cout << "Defensive actions" << endl << endl;
    for(auto action: *payload->DHDefensives->get_data()){
        cout << action->get_name() << endl;
    }
}

void view_offensives_from_list(vector<Offensive*>* offensives){
    cout << "Offensives" << endl;
    for(auto action: *offensives){
        cout << action << endl;
    }
}

void view_defensives_from_list(vector<Defensive*>* defensives){
    cout << "Defensives" << endl;
    for(auto action: *defensives){
        cout << action << endl;
    }
}

 /*---- Templates ----*/

void view_templates(Payload* payload) {


    cout << "Available Roles" << endl;
    cout << (*payload).DHRoles << endl;
    cout << "\nAvailable Species" << endl;
    cout << (*payload).DHSpecies << endl;
}

void view_single_template(int species_index, int role_index, Payload* payload){

    if(species_index != -1){
        cout << payload->DHSpecies->get_data()->at(species_index) << endl;
    }

    else if(role_index != -1){
        cout << payload->DHRoles->get_data()->at(role_index) << endl;
    }
}

void view_shortened_templates(Payload* payload){
    cout << "Available Roles:" << endl;
    for(const auto role: *payload->DHRoles->get_data()){
        cout << '\t' << role->get_name() << endl;
    }

    cout << "\nAvailable Species:" << endl;
    for(const auto species: *payload->DHSpecies->get_data()){
        cout << '\t' << species->get_name() << endl;
    }
}