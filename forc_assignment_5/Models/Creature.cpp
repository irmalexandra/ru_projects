
#include "Creature.h"

Creature::Creature(std::string* name, Species* species): Being(species){
    this->name = *name;
    this->unnatural = species->get_unnatural();
    this->disquiet = get_random_integer(species->get_disquiet_range());

    this->species = species;
    delete name;
    name = nullptr;
}


Creature::Creature(baseIndividualStats* stats, Species* species): Being(stats, (IndividualBaseTemplate*)species){

    this->name = stats->name;
    this->unnatural = stats->unnatural;
    this->disquiet = stats->disquiet;

    this->species = species;
    this->battle_stats->set_as_creature(stats->life, stats->strength, stats->intelligence, stats->disquiet);
};


Species* Creature::get_template(){
    return this->species;
}

void Creature::edit(vector<Creature*>* existing_creatures) {
    enum Stats { Name = 1, Life = 2, Strength = 3, Intelligence = 4, Unnatural = 5, Disquiet = 6, Back = 7};
    int choice = 0;
    int* new_int_value;
    int temp_val = 0;
    std::string* new_name;
    while (true){
        std::cout << "Pick a stat to edit" << std::endl;
        std::cout << "1. Name" << std:: endl;
        std::cout << "2. Life" << std:: endl;
        std::cout << "3. Strength" << std:: endl;
        std::cout << "4. Intelligence" << std:: endl;
        std::cout << "5. Unnatural" << std:: endl;
        std::cout << "6. Disquiet" << std:: endl;
        std::cout << "7. Back" << std:: endl;
        std::cin >> choice;
        switch(choice){
            case Name:
                new_name = new std::string("");
                std::cout << "Enter new name: ";
                std::cin >> *new_name;
                while(get_index(existing_creatures, *new_name) != -1){
                    cout << "Name already taken, choose a different name." << endl;
                    cin >> *new_name;
                }
                this->set_name(new_name);
                break;
            case Life:
                temp_val = get_int_within_range(
                        this->get_template()->get_life_range().min
                        , this->get_template()->get_life_range().max
                        ,"Enter a new value for Life"
                );
                new_int_value = &temp_val;
                this->set_life(new_int_value);
                break;
            case Strength:
                temp_val = get_int_within_range(
                        this->get_template()->get_strength_range().min,
                        this->get_template()->get_strength_range().max,
                        "Enter a new value for Strength"
                );
                new_int_value = &temp_val;
                this->set_strength(new_int_value);
                break;
            case Intelligence:

                temp_val = get_int_within_range(
                        this->get_template()->get_intelligence_range().min
                        , this->get_template()->get_intelligence_range().max
                        ,"Enter a new value for Intelligence"
                );
                new_int_value = &temp_val;
                this->set_intelligence( new_int_value);
                break;
            case Unnatural:
                cout << "Is the creature unnatural?:\n1. yes\n2. no " << endl;
                cin >> temp_val;
                this->set_unnatural(new bool((temp_val == 1)));
                break;
            case Disquiet:
                temp_val = get_int_within_range(
                        this->get_template()->get_disquiet_range().min
                        , this->get_template()->get_disquiet_range().max
                        ,"Enter a new value for Disquiet"
                );
                new_int_value = &temp_val;
                this->set_disquiet(new_int_value);
                break;
            case Back:
                return;
        }
    }
}

bool Creature::get_unnatural() {
    return this->unnatural;
}

int Creature::get_disquiet() {
    return this->disquiet;
}

void Creature::set_unnatural(bool* unnatural) {
    this->unnatural = *unnatural;
}

void Creature::set_disquiet(int *disquiet) {
    this->disquiet = *disquiet;
}

std::ostream& operator<< (std::ostream& out, Creature* creature){
    out << (Being*)(creature);
    if (creature->get_unnatural()){
        out << "Unnatural" << std::endl;
    }else{
        out << "Natural" << std::endl;
    }
    out << "Disquiet: " << creature->get_disquiet() << std::endl;
    return out;
}

void Creature::reset() {
    delete this->battle_stats;
    this->battle_stats = new battleStats();
    this->reset_status();
    this->battle_stats->set_as_creature(this->get_life(), this->get_strength(), this->get_intelligence(), this->get_disquiet());
}

