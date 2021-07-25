#ifndef FORC_PA_5_INDEXFINDER_H
#define FORC_PA_5_INDEXFINDER_H

#include <vector>
#include <string>

template<typename T>
int get_index(std::vector<T*>* data, std::string name){
    int index = -1;
    for (int i = 0; i < data->size(); i++){
        if(data->at(i)->get_name() == name){
            index = i;
            return index;
        }
    }
    return index;
}

#endif //FORC_PA_5_INDEXFINDER_H
