//
// Created by emmik on 05/03/2021.
//

#include "EldritchHorror.h"

EldritchHorror::EldritchHorror(std::string* name, Species* species):Creature(name, species)  {
    this->traumatism = get_random_integer(species->get_traumatism_range());
}

EldritchHorror::EldritchHorror(baseIndividualStats* stats, Species* species):Creature(stats, species) {
    this->traumatism = stats->traumatism;
}

int EldritchHorror::get_traumatism() {
    return this->traumatism;
}

void EldritchHorror::set_traumatism(int *traumatism) {
    this->traumatism = *traumatism;

    delete traumatism;
    traumatism = nullptr;
}

std::ostream& operator<< (std::ostream& out, EldritchHorror* eldritchHorror){
    out << (Creature*)(eldritchHorror);
    out << "Traumatism: " << eldritchHorror->get_traumatism() << std::endl;
    return out;
}

void EldritchHorror::edit() {
    enum Stats { Name = 1, Life = 2, Strength = 3, Intelligence = 4, Unnatural = 5, Disquiet = 6, Traumatism = 7, Back = 8};
    int choice = 0;
    auto new_int_value = new int(0);
    int temp_val = 0;
    std::string* new_name;
    while (true){
        std::cout << "Pick a stat to edit" << std::endl;
        std::cout << "1. Name" << std:: endl;
        std::cout << "2. Life" << std:: endl;
        std::cout << "3. Strength" << std:: endl;
        std::cout << "4. Intelligence" << std:: endl;
//        std::cout << "5. Unnatural" << std:: endl;
//        std::cout << "6. Disquiet" << std:: endl;
        std::cout << "7. Traumatism" << std:: endl;
        std::cout << "8. Back" << std:: endl;
        std::cin >> choice;
        switch(choice){
            case Name:
                new_name = new std::string("");
                std::cout << "Enter new name: ";
                std::cin >> *new_name;
                this->set_name(new_name);
                break;
            case Life:
                temp_val = get_int_within_range(
                        this->get_template()->get_intelligence_range().min
                        , this->get_template()->get_intelligence_range().max
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
                this->set_intelligence(new_int_value);
                break;

//            case Unnatural:
//                cout << "Is the creature unnatural?:\n1. yes\n2. no ";
//                cin >> temp_val;
//                this->set_unnatural(new bool((temp_val == 1)));
//                break;
//            case Disquiet:
//                temp_val = get_int_within_range(
//                        this->get_template()->get_disquiet_range().min
//                        , this->get_template()->get_disquiet_range().max
//                        ,"Enter a new value for Disquiet"
//                );
//                new_int_value = &temp_val;
//                this->set_disquiet(new_int_value);
//                break;

            case Traumatism:
                cout << "Enter a new value for Traumatism (0-3): ";
                temp_val = get_int_within_range(
                        this->get_template()->get_traumatism_range().min
                        , this->get_template()->get_traumatism_range().max
                        ,"Enter a new value for Traumatism"
                );
                new_int_value = &temp_val;
                this->set_traumatism(new_int_value);
                break;
            case Back:
                return;
        }
    }
}


