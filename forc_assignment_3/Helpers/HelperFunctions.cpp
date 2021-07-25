#include "HelperFunctions.h"

using namespace std;


bool comparer(Node* first, Node* second){
    return first->get_data()->get_frequency() > second->get_data()->get_frequency();
}
