#include "HelperFunctions.h"
#include <iostream>

vector<string> split_string(string str, string token){
    vector<string> result;
    while(str.size()){
        int index = str.find(token);
        if(index!=string::npos){
            result.push_back(str.substr(0,index));
            str = str.substr(index+token.size());
            if(str.size()==0)result.push_back(str);
        }else{
            result.push_back(str);
            str = "";
        }
    }
    return result;
}

int get_random_integer(const Range& range) {
    if(range.min == range.max){
        return range.min;
    }
    return int(rand() % (range.max - range.min) + range.min);
}

int get_int_within_range(int lower, int upper, const std::string& display_string){
    int input = -1;
    while(input <= lower || input > upper){
        std::cout << "The range is from " << lower << " to " << upper << std::endl;
        std::cout << display_string << std::endl;
        std::cin >> input;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        if(input == lower){
            return input;
        }
    }
    return input;
}

std::string* gender_picker() {
    int choice;
    enum Gender { Male = 1, Female = 2, Other = 3};
    while(true){
        std::cout << "Pick a gender:\n1. Male\n2. Female\n3. Other"<< std::endl;
        std::cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case Male:
                return new std::string("Male");
            case Female:
                return new std::string("Female");
            case Other:
                return new std::string("Other");
            default:
                std::cout << choice << " is not a valid option";
                break;

        }
    }
}