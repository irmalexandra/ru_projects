#ifndef FORC_PA_5_HELPERFUNCTIONS_H
#define FORC_PA_5_HELPERFUNCTIONS_H

#include <random>
#include "vector"
#include "iostream"
#include "Structs/Stats.h"
using namespace std;

int get_random_integer(const Range& range);
int get_int_within_range(int lower, int upper, const std::string& display_string);
std::string* gender_picker();
bool re_prompt();
std::vector<std::string> split_string(string str, string token = " ");



#endif //FORC_PA_5_HELPERFUNCTIONS_H
