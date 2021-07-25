#ifndef FORC_PA_4_DATAHANDLER_H
#define FORC_PA_4_DATAHANDLER_H
#include "vector"
#include "../Models/Investigator.h"
#include "../Models/Person.h"
#include "../Models/Creature.h"
#include "../Models/EldritchHorror.h"

#include "../Templates/Role.h"
#include "../Templates/Species.h"


template<typename T>
class DataHandler {
public:
    DataHandler(){
        this->data = new std::vector<T*>;
    };

    ~DataHandler(){
        if (this->data != nullptr){
            this->data->erase(this->data->begin(), this->data->end());
            delete this->data;
            this->data = nullptr;
        }

    };

    std::vector<T*>* get_data(){
        return this->data;
    };

    friend std::ostream& operator<< (std::ostream& out, DataHandler<T>* data_handler){
        for (auto const item : *(data_handler->data)){
            out << item << std::endl;
        }
        return out;
    };

private:
    std::vector<T*>* data;

};


#endif //FORC_PA_4_DATAHANDLER_H
