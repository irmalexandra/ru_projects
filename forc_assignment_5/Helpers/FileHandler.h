//
// Created by emmik on 22/03/2021.
//

#ifndef FORC_PA_5_FILEHANDLER_H
#define FORC_PA_5_FILEHANDLER_H
#include <random>
#include <vector>
#include <string>
#include <fstream>
#include <cstring>
#include <iostream>
#include <map>
#include <algorithm>

#include "../Templates/IndividualTemplates/IndividualBaseTemplate.h"
#include "../Templates/IndividualTemplates/Role.h"
#include "../Templates/IndividualTemplates/Species.h"
#include "../Helpers/HelperFunctions.h"
#include "DataHandler.h"
#include "Structs/Payload.h"
#include "IndexFinder.h"

using namespace std;


class FileHandler {
public:
    void load_templates(Payload* payload);
    void save_templates(Payload* payload);
    void load_actions(Payload* payload);
    void save_actions(Payload* payload);
    void load_roster(Payload* payload, string* roster_name);
    void save_roster(Payload* payload, string* roster_name);
};




#endif //FORC_PA_5_FILEHANDLER_H
