#ifndef FORC_PA_3_DATA_H
#define FORC_PA_3_DATA_H

#include <iostream>

using namespace std;

class Data{
public:
    explicit Data() = default;
    explicit Data(unsigned long freq, char value = '\0');
    explicit Data(char value);

    unsigned long get_frequency();

    char get_value();

    virtual ~Data()= default;

    friend ostream& operator<< (ostream& out, const Data *data);

private:
    unsigned long freq;
    char value;
};

#endif //FORC_PA_3_DATA_H
