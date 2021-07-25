#include "Investigator.h"
using namespace std;

Investigator::Investigator(std::string* name, std::string* gender, Role* role_template):
        Person(name, gender, role_template){
    this->terror = get_random_integer(role_template->get_terror_range());
    this->role_template = role_template;
    this->set_is_investigator(new bool(true));
}

Investigator::Investigator(baseIndividualStats *base_stats, Role *base_template)
        :Person(base_stats, base_template) {
    this->terror = base_stats->terror;
    this->role_template = base_template;
    this->set_is_investigator(new bool(true));
}

int Investigator::get_terror() {
    return this->terror;
}

void Investigator::set_terror(int *terror) {
    this->terror = *terror;

    delete terror;
    terror = nullptr;
}

std::ostream& operator<< (std::ostream& out, Investigator* investigator) {
    out << (Person*)(investigator);
    out << "Terror: " << investigator->get_terror() << std::endl;
    return out;
}

void Investigator::edit(vector<Investigator*>* existing_investigators) {
    enum Stats { Name = 1, Life = 2, Strength = 3, Intelligence = 4, Role = 5, Gender = 6, Fear = 7, Terror = 8, Back = 9};
    int choice = 0;
    auto new_int_value = new int(0);
    int temp_val = 0;
    std::string* new_name;
    std::string* new_gender;
    while (true){
        std::cout << "Pick a stat to edit" << std::endl;
        std::cout << "1. Name" << std:: endl;
        std::cout << "2. Life" << std:: endl;
        std::cout << "3. Strength" << std:: endl;
        std::cout << "4. Intelligence" << std:: endl;
        std::cout << "5. Role" << std:: endl;
        std::cout << "6. Gender" << std:: endl;
        std::cout << "7. Fear" << std:: endl;
        std::cout << "8. Terror" << std:: endl;
        std::cout << "9. Back" << std:: endl;
        std::cin >> choice;
        switch(choice){
            case Name:
                new_name = new std::string("");
                std::cout << "Enter new name: ";
                std::cin >> *new_name;
                while(get_index(existing_investigators, *new_name) != -1){
                    cout << "Name already taken, choose a different name." << endl;
                    cin >> *new_name;
                }
                this->set_name(new_name);
                break;
            case Life:
                temp_val = get_int_within_range(
                        this->get_role_template()->get_intelligence_range().min
                        , this->get_role_template()->get_intelligence_range().max
                        ,"Enter a new value for Life"
                );
                new_int_value = &temp_val;
                this->set_life(new_int_value);
                break;
            case Strength:
                temp_val = get_int_within_range(
                        this->get_role_template()->get_strength_range().min,
                        this->get_role_template()->get_strength_range().max,
                        "Enter a new value for Strength"
                );
                new_int_value = &temp_val;
                this->set_strength(new_int_value);
                break;
            case Intelligence:

                temp_val = get_int_within_range(
                        this->get_role_template()->get_intelligence_range().min
                        , this->get_role_template()->get_intelligence_range().max
                        ,"Enter a new value for Intelligence"
                );
                new_int_value = &temp_val;
                this->set_intelligence(new_int_value);
                break;
            case Role:
                cout << "Some cool role picker yes plz" << endl;
                break;
            case Gender:
                this->set_gender(gender_picker());
                break;
            case Fear:
                temp_val = get_int_within_range(
                        this->get_role_template()->get_fear_range().min
                        , this->get_role_template()->get_fear_range().max
                        ,"Enter a new value for Fear"
                );
                new_int_value = &temp_val;
                this->set_fear(new_int_value);
                break;
            case Terror:
                temp_val = get_int_within_range(
                        this->get_role_template()->get_terror_range().min
                        , this->get_role_template()->get_terror_range().max
                        ,"Enter a new value for Terror"
                );
                new_int_value = &temp_val;
                this->set_terror(new_int_value);
                break;
            case Back:
                return;
        }
    }
}

void Investigator::reset() {
    Person::reset();
}
