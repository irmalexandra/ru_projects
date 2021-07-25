#include <algorithm>
#include "FileHandler.h"

void FileHandler::load_templates(Payload* payload){

    char single_line[32] = {};
    string filename = "Resources/template_file.txt";
    cout << "Loading templates from " << filename << "..." << endl;
    speciesStats* stats;

    string name;
    string type;


    auto temp_string_array = new std::vector<std::string>;
    int amount;

    string line_str;
    ifstream fileIn (filename, ios::binary);

    fileIn.getline(single_line, 32);
    line_str = string(single_line);

    amount = stoi(line_str);
    for(int i = 0; i < amount; i++){

        stats = new speciesStats();

        while(line_str == ""){
            fileIn.getline(single_line, 32);
            line_str = string(single_line);
        }

        fileIn.getline(single_line, 32);
        line_str = string(single_line);
        name = split_string(line_str).at(1);
        stats->name = name.substr(0, name.length()-1);

        fileIn.getline(single_line, 32);
        line_str = string(single_line);
        type = line_str.substr(0, line_str.length()-1);
        stats->type = type;


        fileIn.getline(single_line, 32);
        line_str = string(single_line);
        *temp_string_array = split_string(split_string(line_str).at(1), "-");
        stats->life_min = stoi(temp_string_array->at(0));
        stats->life_max = stoi(temp_string_array->at(1));

        fileIn.getline(single_line, 32);
        line_str = string(single_line);
        *temp_string_array = split_string(split_string(line_str).at(1), "-");
        stats->str_min = stoi(temp_string_array->at(0));
        stats->str_max = stoi(temp_string_array->at(1));

        fileIn.getline(single_line, 32);
        line_str = string(single_line);
        *temp_string_array = split_string(split_string(line_str).at(1), "-");
        stats->int_min = stoi(temp_string_array->at(0));
        stats->int_max = stoi(temp_string_array->at(1));

        if(type != "Person"){
            fileIn.getline(single_line, 32);
            line_str = string(single_line);
            if(type.substr(0, type.length()-1) == "Natural"){
                stats->unnatural = false;
            }
            else{
                stats->unnatural = true;
            }
            fileIn.getline(single_line, 32);
            line_str = string(single_line);
            *temp_string_array = split_string(split_string(line_str).at(1), "-");

            stats->dis_min = stoi(temp_string_array->at(0));
            stats->dis_max = stoi(temp_string_array->at(1));
            if(type == ("Eldritch Horror")){

                fileIn.getline(single_line, 32);
                line_str = string(single_line);
                *temp_string_array = split_string(split_string(line_str).at(1), "-");
                stats->trauma_min = stoi(temp_string_array->at(0));
                stats->trauma_max = stoi(temp_string_array->at(1));
                stats->is_eldritch = true;
            }
        }
        if (type == "Person"){
            payload->DHRoles->get_data()->push_back(new Role(stats));
        } else if (type == "Eldritch Horror" || type == "Creature") {
            payload->DHSpecies->get_data()->push_back(new Species(stats));
        }
        fileIn.getline(single_line, 32); // To skip empty lines
        delete stats;
    }

    cout << "Done!" << endl;
}

void FileHandler::save_templates(Payload* payload) {
    string filename = "Resources/template_file.txt";
    cout << "Saving templates to " << filename << "..." << endl;
    ofstream fileout(filename, ios::trunc);
    int amount = 0;
    amount += payload->DHRoles->get_data()->size();
    amount += payload->DHSpecies->get_data()->size();

    fileout << amount << endl;
    fileout << payload->DHSpecies;
    fileout << payload->DHRoles;
    fileout.close();
    cout << "Done!" << endl;
}

void FileHandler::save_roster(Payload* payload, string* roster_name){
    cout << "Saving roster to " << *roster_name << "..." << endl;
    int amount = 0;
    amount += payload->DHInvestigators->get_data()->size();
    amount += payload->DHPersons->get_data()->size();
    amount += payload->DHCreatures->get_data()->size();
    amount += payload->DHEldritch_Horrors->get_data()->size();
    ofstream fileout(*roster_name, ios::trunc);
    fileout << amount << endl;
//    fileout << payload->DHInvestigators->get_data()->size() << endl;
    fileout << payload->DHInvestigators;
//    fileout << payload->DHPersons->get_data()->size() << endl;
    fileout << payload->DHPersons;
//    fileout << payload->DHCreatures->get_data()->size() << endl;
    fileout << payload->DHCreatures;
//    fileout << payload->DHEldritch_Horrors->get_data()->size() << endl;
    fileout << payload->DHEldritch_Horrors;
    fileout.close();
    cout << "Done!" << endl;
}

void FileHandler::load_roster(Payload *payload, string *roster_name) {
    cout << "Loading roster from " << *roster_name << "..." << endl;
    char single_line[32] = {};
    payload->species_map->clear();
    ifstream fileIn (*roster_name, ios::binary);
    if(fileIn.fail()){
        cout << "File not found" << endl;
        return;
    }

    string line_string;
    string type;
    string template_name;

    fileIn.getline(single_line, 32);
    line_string = string(single_line);

    int amount = stoi(line_string);

    for (int i = 0; i < amount; i++){
        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        if(line_string.empty()){
            fileIn.getline(single_line, 32);
            line_string = string(single_line);
        }

        auto stats = new baseIndividualStats();

        while(line_string == "" || line_string == "\n" || line_string == "\r" || line_string == "\n\r" || line_string == "\r\n"){
            fileIn.getline(single_line, 32);
            line_string = string(single_line);
        }

        auto temp = split_string(line_string).at(1);
        stats->name = temp.substr(0, temp.length()-1);

        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        type = line_string.substr(0, line_string.length()-1);
        stats->type = type;

        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        template_name = line_string.substr(0, line_string.length()-1);



        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        stats->life = stoi(split_string(line_string, " ").at(1));

        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        stats->strength = stoi(split_string(line_string).at(1));

        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        stats->intelligence = stoi(split_string(line_string).at(1));

        fileIn.getline(single_line, 32);
        line_string = string(single_line);
        if(type == "Creature" || type == "Eldritch Horror"){
            if(payload->species_map->find(template_name) == payload->species_map->end()){
                payload->species_map->insert(std::pair<string, int>(template_name, 1));
            }
            if(stats->name.substr(0, template_name.length())
               + to_string(payload->species_map->at(template_name)) == stats->name){
                payload->species_map->at(template_name)++;
            }
            auto it = find_if(payload->DHSpecies->get_data()->begin(), payload->DHSpecies->get_data()->end(),
                              [&template_name]( Species* obj) {return obj->get_name() == template_name;});
            auto index = std::distance(payload->DHSpecies->get_data()->begin(), it);
            auto species = payload->DHSpecies->get_data()->at(index);

            // IMPLEMENT TEMPLATE LOOKUP
            stats->unnatural = (type != "Natural");
            fileIn.getline(single_line, 32);
            line_string = string(single_line);
            stats->disquiet = stoi(split_string(line_string).at(1));

            if (type == "Eldritch Horror"){
                fileIn.getline(single_line, 32);
                line_string = string(single_line);
                stats->traumatism = stoi(split_string(line_string).at(1));
                payload->DHEldritch_Horrors->get_data()->push_back(new EldritchHorror(stats, species));

            }
            else{
                payload->DHCreatures->get_data()->push_back(new Creature(stats, species));
            }

        }
        else{
            stats->gender = split_string(line_string).at(1);
            fileIn.getline(single_line, 32);
            line_string = string(single_line);
            stats->fear = stoi(split_string(line_string).at(1));

            auto it = find_if(payload->DHRoles->get_data()->begin(), payload->DHRoles->get_data()->end(),
                              [&template_name]( Role* obj) {return obj->get_name() == template_name;});
            auto index = std::distance(payload->DHRoles->get_data()->begin(), it);

            auto role = payload->DHRoles->get_data()->at(index);

            if(type == "Person"){
                payload->DHPersons->get_data()->push_back(new Person(stats, role));
            }
            if(type == "Investigator"){
                fileIn.getline(single_line, 32);
                line_string = string(single_line);
                stats->terror = stoi(split_string(line_string).at(1));
                payload->DHInvestigators->get_data()->push_back(new Investigator(stats, role));
            }

            fileIn.getline(single_line, 32);
            line_string = string(single_line);
        }

    }
    cout << "Done!" << endl;
}
