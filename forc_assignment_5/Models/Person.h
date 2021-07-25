//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_PERSON_H
#define FORC_PA_5_PERSON_H


#include "Being.h"
#include "string"
#include "iostream"
#include "../Helpers/HelperFunctions.h"
#include "../Templates/IndividualTemplates/Role.h"
#include "../Helpers/IndexFinder.h"

class Person: public Being {
public:
    Person(std::string* name, std::string* gender, Role* base_template);
    Person(baseIndividualStats* stats, Role* base_template);

    std::string get_gender();
    std::string get_role();

    Role* get_role_template();
    int get_fear();
    int get_current_fear();
    void edit(vector<Person*>* existing_persons);

    void set_gender(std::string* gender);
    void set_role(std::string* role);
    void set_fear(int* fear);

    string get_state();

    void reset();

    friend std::ostream& operator<< (std::ostream& out, Person* person);

private:
    std::string role;
    std::string gender;
    int fear;
    Role* role_template;

};


#endif //FORC_PA_5_PERSON_H
