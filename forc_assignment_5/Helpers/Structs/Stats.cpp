#include "Stats.h"

Range::Range(int min, int max){
    this->min = min;
    this->max = max;
};

std::ostream& operator<< (std::ostream& out, Range range){
    out << range.min << "-" << range.max;
    return out;
};