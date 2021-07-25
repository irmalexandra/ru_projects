#ifndef FORC_PA_4_INVESTIGATOR_H
#define FORC_PA_4_INVESTIGATOR_H


#include "Person.h"
#include "iostream"
#include "../Templates/Role.h"

class Investigator: public Person {
public:
    Investigator(std::string* name, std::string* gender, Role* base_template);
    Investigator(baseIndividualStats* base_stats, Role* base_template);
    int get_terror();
    void set_terror(int* terror);

    void edit();

    friend std::ostream& operator<< (std::ostream& out, Investigator* investigator);

private:
    int terror;
    Role* role_template;
};


#endif //FORC_PA_4_INVESTIGATOR_H
