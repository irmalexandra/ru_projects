#include "Data.h"

Data::Data(unsigned long freq, char value){
    this->freq = freq;
    this->value = value;
}
Data::Data(char value) {
    this->value = value;
}

ostream& operator<< (ostream& out, const Data *data){
    out << "f: " << data->freq;
    out << " | ";
    out << "v: " << data->value << endl;
    return out;
}

unsigned long Data::get_frequency() {
    return this->freq;
}

char Data::get_value() {
    return this->value;
}

