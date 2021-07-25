#include "InputHandler.h"

InputHandler::InputHandler() {
    this->individual_creator = new IndividualCreator();
    this->template_creator = new TemplateCreator();
    this->action_creator = new ActionCreator();

    this->file_handler = new FileHandler();


    // Roster
    this->DHInvestigators = new DataHandler<Investigator>;
    this->DHPersons = new DataHandler<Person>;
    this->DHCreatures = new DataHandler<Creature>;
    this->DHEldritchHorrors = new DataHandler<EldritchHorror>;

    // Templates
    this->DHSpecies = new DataHandler<Species>;
    this->DHRoles = new DataHandler<Role>;
    this->DHOffensives = new DataHandler<Offensive>;
    this->DHDefensives = new DataHandler<Defensive>;
    // Species Map
    this->species_map = new map<string, int>;


    // Payload
    this->payload = new Payload(
            this->DHInvestigators,
            this->DHPersons,
            this->DHCreatures,
            this->DHEldritchHorrors,
            this->DHSpecies,
            this->DHRoles,
            this->DHOffensives,
            this->DHDefensives,
            this->species_map
    );
    this->file_handler->load_actions(this->payload);

    this->file_handler->load_templates(this->payload);
}

InputHandler::~InputHandler() {
    delete this->individual_creator;
    delete this->action_creator;
    delete this->DHInvestigators;
    delete this->DHCreatures;
    delete this->DHEldritchHorrors;
    delete this->DHPersons;
    delete this->DHSpecies;
    delete this->DHRoles;
    delete this->species_map;
    delete this->file_handler;
    delete this->payload;

    this->individual_creator = nullptr;
    this->action_creator = nullptr;
    this->DHInvestigators = nullptr;
    this->DHPersons = nullptr;
    this->DHCreatures = nullptr;
    this->DHEldritchHorrors = nullptr;
    this->DHSpecies = nullptr;
    this->DHRoles = nullptr;
    this->species_map = nullptr;
    this->file_handler = nullptr;
    this->payload = nullptr;
}

void InputHandler::main_menu() {
    int choice;
    string filename;
    string folder = "Saves/";
    while(true){
        cout << "1. Templates\n2. Individuals\n3. Actions\n4. Battle Simulator\n0. Quit" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }

        switch (choice) {
            case 1:
                template_menu(this->file_handler, this->template_creator, this->payload);
                break;
            case 2:
                individual_menu(this->file_handler, this->payload, this->individual_creator);
                break;
            case 3:
                action_menu(this->file_handler, this->action_creator, this->payload);
                break;
            case 4:
                battle_menu(this->payload, this->file_handler);
                break;
            case 0:
                return;
            default:
                cout << choice << " is not an option" << endl;
                break;
        }
    }
}
